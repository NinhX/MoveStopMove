using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private MapData[] maps;
    [SerializeField] private Player player;

    public int MapQty => maps.Length;
    public string KillerName => player.killerName;
    public int PlayerScore => player.Scores;
    public int MaxBot => maxBot;

    private ObjectPool pool => ObjectPool.Instance;
    private GameManager gameManager => GameManager.Instance;
    private MapData currentMap;
    private GameObject mapObject;
    private Transform[] listPointSpawn;
    private List<Bot> listBot = new();

    private int botQty;
    private int maxBot;
    private WaitForSeconds timeReSpawn = new(Constant.TIME_BOT_RESPAWN);
    private WaitForSeconds timeNewUpdate = new(Constant.TIME_NEW_UPDATE);

    private IEnumerator IEUpdate()
    {
        while (true)
        {
            if (gameManager.IsState(GameState.Play))
            {
                for (int i = listBot.Count - 1; i >= 0; i--)
                {
                    Bot bot = listBot[i];
                    bot.NewUpdate();
                }
            }
            yield return timeNewUpdate;
        }
    }

    public void OnInit()
    {
        listPointSpawn = currentMap.ListPointSpawn;
        botQty = 0;
        maxBot = currentMap.MaxBot;
    }

    public void GenerateMap(int level)
    {
        if (level < 0 || level > MapQty)
        {
            level = 0;
        }

        DestroyMap();
        currentMap = maps[level];
        mapObject = Instantiate(currentMap.gameObject);
        OnInit();
    }

    public void AfterGenerateMap()
    {
        navMeshSurface.BuildNavMesh();
        SpawCharacter();
        StartCoroutine(IEUpdate());
    }

    private void SpawCharacter()
    {
        listBot.Clear();
        for (int i = 0; i < listPointSpawn.Length; i++)
        {
            RegisterBot(listPointSpawn[i].position);
        }
        player.OnInit();
        player.SetPositon(currentMap.PointPlayer);
        player.OnDeadAction = OnPlayerDead;
        AddEvent(player);
    }

    public void RegisterBot(Vector3 pointSpawn)
    {
        Bot bot = pool.Spawn<Bot>(PoolType.Character, pointSpawn, Quaternion.identity);
        bot.OnInit();
        bot.OnDeadAction = OnBotDead;
        AddEvent(bot);

        listBot.Add(bot);
        botQty++;
        UIManager.Instance.AddPanelEnemy(bot.Tf);
    }

    public void UnRegisterBot(Bot bot)
    {
        botQty--;
        maxBot--;
        listBot.Remove(bot);

        if (maxBot == 0)
        {
            gameManager.ChangeState(GameState.Victory);
            player.VictoryAction();
        }
    }

    public Character GetNearbyCharacter(Transform tf, float range)
    {
        Character character;
        if (tf != player.Tf && Vector3.Distance(player.Tf.position, tf.position) <= range)
        {
            character = player;
        }
        else
        {
            character = GetNearbyBot(tf, range);
        }

        return character;
    }

    private void OnPlayerDead(Character character)
    {
        gameManager.ChangeState(GameState.Lose);
    }

    private void OnBotDead(Character character)
    {
        UnRegisterBot((Bot)character);
        StartCoroutine(IESpawCharacter());
    }

    private IEnumerator IESpawCharacter()
    {
        while (true)
        {
            yield return timeReSpawn;
            if (!gameManager.IsState(GameState.Play) || botQty >= maxBot)
            {
                yield break;
            }

            Vector3 pointRandom = listPointSpawn[Random.Range(0, listPointSpawn.Length)].position;
            if (CheckSpaw(pointRandom))
            {
                RegisterBot(pointRandom);
                yield break;
            }
        }
    }

    private Character GetNearbyBot(Transform tf, float range)
    {
        Character character = null;
        for (int i = listBot.Count - 1; i >= 0; i--)
        {
            Character bot = listBot[i];
            if (tf != bot.Tf && Vector3.Distance(bot.Tf.position, tf.position) <= range)
            {
                character = bot;
                break;
            }
        }
        return character;
    }

    private void DestroyMap()
    {
        StopAllCoroutines();
        NavMesh.RemoveAllNavMeshData();
        pool.CollectAll();
        Destroy(mapObject);
    }

    private void AddEvent(Character character)
    {
        character.OnDeadAction += gameManager.OnCharacterDead;
        character.OnAttackAction = gameManager.OnCharacterAttack;
    }

    private bool CheckSpaw(Vector3 point)
    {
        bool result = true;
        for (int i = listBot.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(listBot[i].Tf.position, point) < 3)
            {
                result = false;
                break;
            }
        }
        if (Vector3.Distance(player.Tf.position, point) < 3)
        {
            result = false;
        }
        return result;
    }
}
