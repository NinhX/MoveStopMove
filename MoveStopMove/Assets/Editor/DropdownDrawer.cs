using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DropdownAttribute))]
public class DropdownDrawer : PropertyDrawer
{ 
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DropdownAttribute dropdownAttribute = attribute as DropdownAttribute;
        string[] dropdown = dropdownAttribute.Dropdown;

        if (dropdown.Length > 0)
        {
            int selectedIndex = Array.IndexOf(dropdown, property.stringValue);
            selectedIndex = selectedIndex >= 0 ? selectedIndex : 0;
            selectedIndex = EditorGUI.Popup(position, property.name, selectedIndex, dropdown);
            property.stringValue = dropdown[selectedIndex];
        }
        else
        {
            EditorGUI.LabelField(position, "tag not valid , dropdown empty.");
        }
    }
}