using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static GameState currentState;

    private UIManager uiManager => UIManager.Instance;
    private AudioManager audioManager => AudioManager.Instance;
    private LevelManager levelManager => LevelManager.Instance;

    void Awake()
    {
        Application.targetFrameRate = 120;
        Time.timeScale = 1;
    }

    void Start()
    {
        OnInit();
        ChangeState(GameState.Start);
    }

    public void ChangeState(GameState newState)
    {
        ExitState();
        currentState = newState;
        switch (newState)
        {
            case GameState.Start:
                HandleStart();
                break;
            case GameState.Play:
                HandlePlayGame();
                break;
            case GameState.Pause:
                HandlePauseGame();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
            case GameState.Lose:
                HandleLose();
                break;
        }
    }

    public bool IsState(GameState state)
    {
        return currentState == state;
    }

    public void OnInit()
    {
    }

    public void OnCharacterDead(Character characterDead)
    {
        uiManager.ReloadUI<PlayPanel>();
        if (characterDead.IsModelVisible)
        {
            audioManager.PlaySound(SoundType.Die);
        }
    }

    public void OnCharacterAttack(Character character, IWeapon iWeapon)
    {
        if (character.IsModelVisible)
        {
            audioManager.PlaySound(SoundType.WeaponThrow);
        }
    }

    private void ExitState()
    {
        if (IsState(GameState.Pause))
        {
            Time.timeScale = 1;
        }
    }

    private void HandleStart()
    {
        uiManager.CloseAllPanel();
        uiManager.DisableJoystick();
        StartCoroutine(IEStart());
    }

    private void HandlePlayGame()
    {
        uiManager.CloseAllPanel();
        uiManager.OpenUI<PlayPanel>();
        uiManager.OpenUI<EnemyPanel>();
        uiManager.ReloadUI<PlayPanel>();
        uiManager.EnableJoystick();
    }

    private void HandlePauseGame()
    {
        uiManager.CloseAllPanel();
        uiManager.OpenUI<PausePanel>();
        uiManager.DisableJoystick();
        Time.timeScale = 0;
    }

    private void HandleVictory()
    {
        uiManager.CloseAllPanel();
        uiManager.DisableJoystick();
        uiManager.OpenUI<VictoryPanel>();
        CamFlower.Instance.OnInit();
    }

    private void HandleLose()
    {
        uiManager.CloseAllPanel();
        uiManager.DisableJoystick();
        uiManager.OpenUI<LosePanel>();
    }

    private IEnumerator IEStart()
    {
        levelManager.LoadLevel();
        uiManager.OpenUI<LoadingPanel>();

        while (levelManager.isLoading)
        {
            yield return new WaitForEndOfFrame();
        }

        uiManager.CloseUI<LoadingPanel>();
        CamFlower.Instance.OnInit();
        ChangeState(GameState.Play);
    }
}
