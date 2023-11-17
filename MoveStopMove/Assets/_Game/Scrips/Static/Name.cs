using System.Collections.Generic;
using UnityEngine;

public static class Name
{
    private static List<string> names = new();

    public static string RandomName()
    {
        if (names.Count == 0)
        {
            names = ReadName();
        }
        return names[Random.Range(0, names.Count)];
    }

    private static List<string> ReadName()
    {
        string name = "";
        List<string> names = new();
        TextAsset textMap = Resources.Load<TextAsset>("Name");
        foreach (char text in textMap.text)
        {
            if (text != 13 && text != 10)
            {
                name += text;
            }
            else if (name.Length != 0)
            {
                names.Add(name);
                name = "";
            }
        }
        return names;
    }
}
