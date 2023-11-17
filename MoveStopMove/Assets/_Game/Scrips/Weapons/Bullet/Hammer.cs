using System.Collections;
using UnityEngine;

public class Hammer : AbstractBullet
{
    private Vector3 forward;
    private float timeAlive = 2;
    private float rorateSpeed = 900;

    public override void Attack(Vector3 targetPositon)
    {
        targetPositon.y += Tf.localPosition.y;
        Tf.LookAt(targetPositon);
        forward = Tf.forward;
        SetTimeAlive(timeAlive);
        StartCoroutine(IEUpdate());
    }

    private IEnumerator IEUpdate()
    {
        while (gameObject.activeInHierarchy)
        {
            Tf.position += speed * Time.deltaTime * forward;
            Tf.Rotate(Vector3.up, rorateSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
