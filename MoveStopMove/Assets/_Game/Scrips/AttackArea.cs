using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] Character owner;

    public float Range => range;

    private float range;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.CHARACTER_TAG))
        {
            Character otherCharacter = Cache.GenCharacter(other);
            if (otherCharacter != owner)
            {
                owner.AddTaget(otherCharacter);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.CHARACTER_TAG))
        {
            Character otherCharacter = Cache.GenCharacter(other);
            if (otherCharacter != owner)
            {
                owner.RemoveTaget(otherCharacter);
            }
        }
    }

    public void SetRange(float range)
    {
        this.range = range;
        transform.localScale = Vector3.one * range;
    }
}
