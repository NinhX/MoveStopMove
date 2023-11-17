using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TagPool : EditorWindow
{
    public static List<string> Dropdown;

    [MenuItem("Tool/TagPool")]
    private static void Tab()
    {
        TagPool tagPool = GetWindow<TagPool>();
        tagPool.minSize = new Vector2(200, 200);
        tagPool.maxSize = new Vector2(600, 600);
    }

    void OnGUI()
    {
        AddTextField();
        if (GUILayout.Button("AddTag"))
        {
            AddTextField();
        }
    }

    private void AddTextField()
    {
        EditorGUILayout.TextField("Option");
    }
}
