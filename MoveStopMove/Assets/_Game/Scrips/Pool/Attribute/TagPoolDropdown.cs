using System.Collections.Generic;
using UnityEngine;

public class TagPoolDropdown : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] private List<string> dropdown;

    public static List<string> Dropdown;


    public void OnBeforeSerialize()
    {
        Refresh();
    }

    public void OnAfterDeserialize()
    {
        Refresh();
    }
    private void Refresh()
    {
        if (dropdown != null)
        {
            Dropdown = new(dropdown);
        }
    }
}
