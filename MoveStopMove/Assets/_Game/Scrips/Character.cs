using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Character : AbstractGameUnit
{
    [SerializeField] protected CharacterScripableObject characterScripable;
    [SerializeField] private AttackArea attackArea;

    public Action<Character, IWeapon> OnAttackAction;
    public Action<Character> OnDeadAction;

    public bool Alive => alive;
    public bool CanAttack => canAttack;
    public bool Attacking => attacking;
    public bool IsModelVisible => isModelVisible;
    public int Scores => scores;
    public string CharacterName => characterName;
    public int KillerId => killer.ID;

    protected Character target;
    protected Character killer;
    protected Skin skin;
    protected Animator animator;
    protected int scores;
    protected string characterName;

    private AbstractListSkinScipableObject<WeaponType> listWeaponScripable;
    private AbstractListSkinScipableObject<PantType> listPantScripable;
    private AbstractListSkinScipableObject<SkinType> listSkinScripable;
    private AbstractListSkinScipableObject<HatType> listHatScripable;

    private GameManager gameManager => GameManager.Instance;
    private IWeapon iWeapon;
    private Coroutine attackCoroutine;
    private GameObject weapon;
    private bool alive;
    private bool isModelVisible;
    private bool attacking;
    private bool canAttack;
    private float delayAttack = 0.4f;
    private float attackCooldown = 1f;
    private float range = 7;
    private List<Character> listTarget = new();
    // key: Animation Name, Value: Animation Time
    private Dictionary<string, float> listAnimTime = new();

    private WaitForSeconds waitDelayAttack;
    private WaitForSeconds waitAttackCooldown;

    private void Awake()
    {
        listWeaponScripable = characterScripable.WeaponScripable;
        listPantScripable = characterScripable.PantScipable;
        listSkinScripable = characterScripable.SkinScipable;
        listHatScripable = characterScripable.HatScripable;
        InitModel();

        waitDelayAttack = new WaitForSeconds(delayAttack);
        waitAttackCooldown = new WaitForSeconds(attackCooldown);
    }

    public virtual void InitModel()
    {
        skin = Instantiate(characterScripable.ModelPrefab, Tf);
        SetAnimTime();
    }

    public virtual void OnInit()
    {
        alive = true;
        attacking = false;
        canAttack = true;
        characterName = "";
        scores = 0;
        Tf.localScale = Vector3.one;
        CancelInvoke();
        ResetAllAnim();
        listTarget = new();
        SetTarget(null);
        gameObject.SetActive(true);
    }

    public virtual void OnDead()
    {
        alive = false;
        CancelAttack();
        animator.SetBool(Constant.DEAD_ANIM, true);
        OnDeadAction?.Invoke(this);
        OnDesPawn(listAnimTime[Constant.DEAD_ANIM_NAME]);
    }

    public virtual void Move()
    {
        animator.SetBool(Constant.IDLE_ANIM, false);
        animator.SetBool(Constant.ATTACK_ANIM, false);
        CancelAttack();
        canAttack = true;
    }

    public virtual void Stop()
    {
        animator.SetBool(Constant.IDLE_ANIM, true);
        if (canAttack && target != null)
        {
            Attack();
        }
    }

    public virtual void SetTarget(Character target)
    {
        this.target = target;
    }

    public virtual void Hit(Character killer)
    {
        if (Alive && gameManager.IsState(GameState.Play))
        {
            this.killer = killer;
            killer.OnKillEnemy(this);
            OnDead();
        }
    }

    public virtual void OnKillEnemy(Character enemy)
    {
        ScaleUp();
        AddCoin(1 + enemy.Scores);
    }

    public virtual void AddCoin(int coin)
    {
        scores += coin;
    }

    public void Visible()
    {
        isModelVisible = true;
        skin.EnableSkin();
    }

    public void Hidden()
    {
        isModelVisible = false;
        skin.DisableSkin();
    }

    public void AddTaget(Character target)
    {
        if (target.Alive)
        {
            listTarget.Add(target);
            target.OnDeadAction += OnTargertDead;
            if (listTarget.Count == 1)
            {
                SetTarget(target);
            }
        }
    }

    public void RemoveTaget(Character target)
    {
        if (target != null)
        {
            target.OnDeadAction -= OnTargertDead;
            listTarget.Remove(target);
            if (this.target == target)
            {
                SetTarget(NextTarget());
            }
        }
    }

    public void ScaleUp()
    {
        if (Tf.localScale.x < Constant.SCALE_UP_MAX)
        {
            Tf.localScale += Vector3.one * Constant.SCALE_UP;
        }
    }

    public void ChangeWeapon(WeaponType weaponTag)
    {
        IGameObjectScripable weaponScripable = (IGameObjectScripable)listWeaponScripable.GetScripable(weaponTag);
        weapon = weaponScripable.CreateGameObject();
        iWeapon = weapon.GetComponent<IWeapon>();
        iWeapon.Owner = this;
        skin.SetWeapon(weapon);
        attackArea.SetRange(range);
    }

    public void ChangeHat(HatType hatType)
    {
        IGameObjectScripable hatScripable = (IGameObjectScripable)listHatScripable.GetScripable(hatType);
        GameObject hat = hatScripable.CreateGameObject();
        skin.SetHat(hat);
    }

    public void ChangePant(PantType pantType)
    {
        IMaterialScripable pantScripable = (IMaterialScripable)listPantScripable.GetScripable(pantType);
        Material pant = pantScripable.GetMaterial();
        skin.SetPant(pant);
    }

    public void ChangeSkin(SkinType skinType)
    {
        IMaterialScripable skinScripable = (IMaterialScripable)listSkinScripable.GetScripable(skinType);
        Material skinMaterial = skinScripable.GetMaterial();
        skin.SetSkin(skinMaterial);
    }

    public bool CheckTarget()
    {
        return target != null;
    }

    public void ResetAllAnim()
    {
        animator.SetBool(Constant.ATTACK_ANIM, false);
        animator.SetBool(Constant.DANCE_ANIM, false);
        animator.SetBool(Constant.DEAD_ANIM, false);
        animator.SetBool(Constant.IDLE_ANIM, false);
        animator.SetBool(Constant.ULTI_ANIM, false);
        animator.SetBool(Constant.WIN_ANIM, false);
    }

    protected Character NextTarget()
    {
        if (listTarget.Count != 0)
        {
            return GetNearestTarget();
        }
        return null;
    }

    private void Attack()
    {
        if (!attacking)
        {
            attackCoroutine = StartCoroutine(IEAttack());
        }
    }

    private void CancelAttack()
    {
        if (attacking)
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }
            EndAttack();
        }
    }

    private IEnumerator IEAttack()
    {
        if (CheckTarget())
        {
            // look target and start animation attack
            attacking = true;
            Character target = this.target;
            animator.SetBool(Constant.ATTACK_ANIM, true);
            LookTarget(target.Tf.position);

            yield return waitDelayAttack;
            // attack
            canAttack = false;
            iWeapon.Attack(Tf, target.Tf);
            weapon.SetActive(false);
            OnAttackAction?.Invoke(this, iWeapon);

            yield return waitAttackCooldown;
            EndAttack();
        }
    }

    private void SetAnimTime()
    {
        animator = skin.Anim;

        AnimationClip[] animationClips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip animationClip in animationClips)
        {
            string animationName = animationClip.name;
            listAnimTime.Add(animationName, animationClip.length);
            if (animationName == Constant.ATTACK_ANIM_NAME)
            {
                attackCooldown = animationClip.length - delayAttack;
            }
        }
    }
    
    private void EndAttack()
    {
        animator.SetBool(Constant.ATTACK_ANIM, false);
        if (isModelVisible)
        {
            weapon.SetActive(true);
        }
        attacking = false;
    }

    private Character GetNearestTarget()
    {
        Character nearestTarget = listTarget[0];
        float minDistance = Vector3.Distance(Tf.position, nearestTarget.Tf.position);
        for (int i = 1; i < listTarget.Count; i++)
        {
            Character target = listTarget[i];
            float tmpDistance = Vector3.Distance(Tf.position, target.Tf.position);
            if (tmpDistance < minDistance)
            {
                nearestTarget = target;
                minDistance = tmpDistance;
            }
        }
        return nearestTarget;
    }

    private void OnTargertDead(Character enemy)
    {
        RemoveTaget(enemy);
    }

    private bool IsVectorZero(Vector3 vector3)
    {
        return Mathf.Abs(vector3.z) < 0.1f && Mathf.Abs(vector3.x) < 0.1f;
    }

    private void LookTarget(Vector3 target)
    {
        target.y = Tf.position.y;
        Vector3 rotation = target - Tf.position;
        if (!IsVectorZero(rotation))
        {
            Tf.rotation = Quaternion.LookRotation(rotation);
        }
    }
}
