using UnityEngine;

public abstract class AbstractUI : MonoBehaviour
{
    public abstract void OnInit();
    public abstract void Open();
    public abstract void Close();
    public abstract void ReloadUI();
}
