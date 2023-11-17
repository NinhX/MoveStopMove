using UnityEngine;

public class UziBullet : AbstractBullet
{
    [SerializeField] private Rigidbody rb;

    public override void OnInit()
    {
        base.OnInit();
        SetTimeAlive(2);
    }

    public override void Attack(Vector3 direction)
    {
        direction.y = Tf.position.y;
        rb.velocity = direction * speed;
    }
}
