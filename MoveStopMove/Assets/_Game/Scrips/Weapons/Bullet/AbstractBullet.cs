using System.Collections;
using UnityEngine;

public abstract class AbstractBullet : AbstractGameUnit
{
    [Range(0f, 10f)][SerializeField] private float speedDefault;

    public Character Owner { get; set; }

    protected float speed;

    private void OnEnable()
    {
        OnInit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.CHARACTER_TAG))
        {
            CheckHit(other);
        }
        else if (other.CompareTag(Constant.MAP_TAG))
        {
            Stop();
        }
    }

    public abstract void Attack(Vector3 targetPositon);

    public virtual void OnInit()
    {
        speed = speedDefault;
    }

    public void ChangeSpeed(float speed)
    {
        this.speed = Mathf.Clamp(speed, 0f, 10f);
    }

    public void SetTimeAlive(float time)
    {
        OnDesPawn(time);
    }

    protected virtual void Stop()
    {
        ChangeSpeed(0);
    }

    private void CheckHit(Collider other)
    {
        Character otherCharacter = other.gameObject.GetComponent<Character>();
        if (otherCharacter != Owner)
        {
            HitEnemy(otherCharacter);
        }
    }

    private void HitEnemy(Character enemy)
    {
        enemy.Hit(Owner);
        CancelInvoke();
        OnDesPawn();
    }
}
