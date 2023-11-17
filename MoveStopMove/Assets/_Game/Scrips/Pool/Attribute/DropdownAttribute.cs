using System;
using System.Collections.Generic;
using UnityEngine;

public class DropdownAttribute : PropertyAttribute
{ 
    public string[] Dropdown { get; private set; }

    public DropdownAttribute(Type type, string propertyName)
    {
        List<string> listDropdown = type.GetField(propertyName).GetValue(type) as List<string>;
        Dropdown = listDropdown != null ? listDropdown.ToArray() : new string[] { };
    }
}
