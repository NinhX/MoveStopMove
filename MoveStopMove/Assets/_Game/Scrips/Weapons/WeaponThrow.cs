using UnityEngine;

public class WeaponThrow : MonoBehaviour, IWeapon
{
    [SerializeField] PoolType bulletType;

    public Character Owner { get; set; }
    private ObjectPool pool => ObjectPool.Instance;

    public void Attack(Transform player, Transform target)
    {
        AbstractBullet bullet = pool.Spawn<AbstractBullet>(bulletType, transform.position);
        bullet.Owner = Owner;
        bullet.Tf.localScale = player.localScale;
        bullet.Attack(target.position);
    }
}
