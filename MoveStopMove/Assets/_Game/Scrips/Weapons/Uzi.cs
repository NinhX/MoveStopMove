using UnityEngine;

public class Uzi : MonoBehaviour, IWeapon
{
    public Character Owner { get; set; }
    private ObjectPool pool => ObjectPool.Instance;

    private Transform tf => transform; 

    public void Attack(Transform player, Transform target)
    {
        Vector3 direction = target.position - tf.position;
        UziBullet bullet = pool.Spawn<UziBullet>(PoolType.UziBullet, tf.position, player.rotation);
        bullet.Owner = Owner;
        bullet.Tf.localScale = player.localScale;
        bullet.Attack(direction);
    }
}
