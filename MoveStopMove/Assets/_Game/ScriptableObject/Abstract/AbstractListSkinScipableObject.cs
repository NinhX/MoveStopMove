using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractListSkinScipableObject<T> : ScriptableObject where T : Enum
{
    [SerializeField] AbstractSkinScripableObject<T>[] skins;

    private Dictionary<T, AbstractSkinScripableObject<T>> dictSkin;

    public Dictionary<T, AbstractSkinScripableObject<T>> DictSkin => dictSkin ??= skins.ToDictionary(
                (Func<AbstractSkinScripableObject<T>, T>)(                key => key.GetSkinType()),
                value => value
            );

    public AbstractSkinScripableObject<T> GetScripable(T skinType)
    {
        if (DictSkin.TryGetValue(skinType, out AbstractSkinScripableObject<T> skin))
        {
            return skin;
        }
        return DictSkin.First().Value;
    }

    public T GetRandomKey()
    {
        return DictSkin.RandomKey();
    }

    public T Next(T currentType)
    {
        return DictSkin.NextKey(currentType);
    }

    public T Prev(T currentType)
    {
        return DictSkin.PrevKey(currentType);
    }
}