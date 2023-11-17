using UnityEngine;

public class VisibleArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.CHARACTER_TAG))
        {
            Cache.GenCharacter(other).Visible();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.CHARACTER_TAG))
        {
            Cache.GenCharacter(other).Hidden();
        }
    }
}
