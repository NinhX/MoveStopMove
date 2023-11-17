using UnityEngine;

public class Arrow : AbstractBullet
{
    void FixedUpdate()
    {
        transform.position += speed * Time.fixedDeltaTime *  transform.forward;
    }

    public override void Attack(Vector3 targetPositon)
    {
    }
}
