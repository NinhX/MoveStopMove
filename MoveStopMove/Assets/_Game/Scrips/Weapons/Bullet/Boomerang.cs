using System.Collections;
using UnityEngine;

public class Boomerang : AbstractBullet
{
    [SerializeField] private float rorateSpeed;

    private float time = 2;
    private float timeAlive = 4;
    private Vector3 endPoint;

    public override void Attack(Vector3 targetPositon)
    {
        targetPositon.y = Tf.position.y;
        endPoint = targetPositon;
        SetTimeAlive(timeAlive);
        StartCoroutine(IEBack());
        StartCoroutine(IEUpdate());
    }

    private IEnumerator IEBack()
    {
        yield return new WaitForSeconds(time);
        Vector3 charaterPoison = Owner.Tf.position;
        endPoint = new(charaterPoison.x, Tf.position.y, charaterPoison.z);
    }
    private IEnumerator IEUpdate()
    {;
        while (gameObject.activeInHierarchy)
        {
            Tf.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
            transform.Rotate(Vector3.up, rorateSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
