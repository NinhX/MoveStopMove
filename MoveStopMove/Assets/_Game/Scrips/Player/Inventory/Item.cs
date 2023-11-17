using System;
using UnityEngine;

[Serializable]
public class Item<T> where T : Enum
{
    public T type;

    public int number = 0;

    public Item(T type)
    {
        this.type = type;
    }

    public Item(T type, int number)
    {
        this.type = type;
        this.number = number;
    }

    public void CopyFrom(Item<T> item)
    {
        type = item.type;
        number = item.number;
    }

    public Item<T> Clone()
    {
        string json = JsonUtility.ToJson(this);
        return JsonUtility.FromJson<Item<T>>(json);
    }
}
