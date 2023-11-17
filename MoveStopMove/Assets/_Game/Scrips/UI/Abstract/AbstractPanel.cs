using UnityEngine;

public abstract class AbstractPanel : MonoBehaviour
{
    public virtual void Open()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }

    public virtual void Close()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
