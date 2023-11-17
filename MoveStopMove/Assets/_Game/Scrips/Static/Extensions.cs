using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    private static Random random = new();

    public static T RandomEnum<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(random.Next(values.Length));
    }

    public static Tkey NextKey<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey currentKey) 
    {
        Tkey[] keys = dict.Keys.ToArray();
        int currentIndex = Array.IndexOf(keys, currentKey);
        int nextIndex = currentIndex == keys.Length - 1 ? currentIndex : currentIndex + 1;

        return keys[nextIndex];
    }

    public static Tkey PrevKey<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey currentKey)
    {
        Tkey[] keys = dict.Keys.ToArray();
        int currentIndex = Array.IndexOf(keys, currentKey);
        int prevIndex = currentIndex <= 0 ? 0 : currentIndex - 1;

        return keys[prevIndex];
    }

    public static Tkey RandomKey<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict)
    {
        return dict.Keys.ElementAt(random.Next(dict.Count));
    }

    public static T Next<T> (this T currentValue) where T : Enum
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        int currentIndex = Array.IndexOf(values, currentValue);
        int nextIndex = currentIndex == values.Length - 1 ? currentIndex : currentIndex + 1;

        return (T)values.GetValue(nextIndex);
    }

    public static T Prev<T>(this T currentValue) where T : Enum
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        int currentIndex = Array.IndexOf(values, currentValue);
        int prevIndex = currentIndex <= 0 ? 0 : currentIndex - 1;

        return (T)values.GetValue(prevIndex);
    }
}
