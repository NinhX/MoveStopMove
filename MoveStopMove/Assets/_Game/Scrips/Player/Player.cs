using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject targetCircle;

    public string killerName => killer.CharacterName;

    private Joystick joystick;
    private float speedRotaion = 0.2f;
    private float speed;
    private bool isVictory;

    private void FixedUpdate()
    {
        if (!Alive || isVictory)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        if (joystick.Direction3 != Vector3.zero)
        {
            Move();
        }
        else
        {
            Stop();
        }
    }

    public override void InitModel()
    {
        base.InitModel();

        PlayerInventory.TryGetItemSelected(out WeaponType weaponType);
        ChangeWeapon(weaponType);
        PlayerInventory.TryGetItemSelected(out HatType hatType);
        ChangeHat(hatType);
        PlayerInventory.TryGetItemSelected(out PantType pantType);
        ChangePant(pantType);
        PlayerInventory.TryGetItemSelected(out SkinType skinType);
        ChangeSkin(skinType);
    }

    public override void OnInit()
    {
        base.OnInit();
        joystick = UIManager.Instance.GetJoystick();
        isVictory = false;
        characterName = PlayerPrefs.GetString(Constant.NAME_PLAYER_PREF, name);
        scores = PlayerInventory.GetItem(ItemType.Coin).number;
        speed = characterScripable.Speed;
        Visible();
    }

    public override void SetTarget(Character taret)
    {
        base.SetTarget(taret);
        if (taret != null)
        {
            targetCircle.SetActive(true);
            targetCircle.transform.position = target.transform.position;
            targetCircle.transform.SetParent(taret.transform);
        }
        else
        {
            targetCircle.SetActive(false);
        }
    }

    public override void Move()
    {
        base.Move();
        Vector3 directon = joystick.Direction3;
        Quaternion look = Quaternion.LookRotation(directon);
        directon *= speed;
        directon.y = rb.velocity.y;
        rb.velocity = directon;
        transform.rotation = Quaternion.Lerp(transform.rotation, look, speedRotaion);
    }

    public override void Stop()
    {
        base.Stop();
        rb.velocity = new(0, rb.velocity.y, 0);
    }

    public override void OnKillEnemy(Character enemy)
    {
        base.OnKillEnemy(enemy);
        CamFlower.Instance.SpaceUp();
    }

    public override void AddCoin(int coin)
    {
        base.AddCoin(coin);
        PlayerInventory.AddOrUpdateItem(ItemType.Coin, scores);
    }

    public void SetPositon(Vector3 positon)
    {
        transform.position = positon;
    }

    public void VictoryAction()
    {
        isVictory = true;
        animator.SetBool(Constant.WIN_ANIM, true);
        transform.rotation = Quaternion.LookRotation(Vector3.back);
    }
}
