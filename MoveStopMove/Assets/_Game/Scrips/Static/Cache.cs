using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, Character> dictCharacter = new();

    public static Character GenCharacter(Collider collider)
    {
        Character character;
        if (dictCharacter.ContainsKey(collider))
        {
            character = dictCharacter[collider];
        }
        else
        {
            character = collider.GetComponent<Character>();
            dictCharacter.Add(collider, character);
        }
        return character;
    }
}
