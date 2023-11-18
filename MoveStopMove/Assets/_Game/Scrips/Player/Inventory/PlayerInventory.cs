using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInventory
{
    private static Dictionary<Type, AbstractCollection> dictItemCollection = new()
    {
        { typeof(WeaponType), new ItemCollection<WeaponType>(Constant.WEAPON_PLAYER_PREF)},
        { typeof(HatType), new ItemCollection<HatType>(Constant.HAT_PLAYER_PREF)},
        { typeof(PantType), new ItemCollection<PantType>(Constant.PANT_PLAYER_PREF)},
        { typeof(SkinType), new ItemCollection<SkinType>(Constant.SKIN_PLAYER_PREF)},
        { typeof(ItemType), new ItemCollection<ItemType>(Constant.ITEM_PLAYER_PREF)},
    };

    public static void Init()
    {
        if (!TryGetItemSelected<WeaponType>(out _))
        {
            AddItem(ItemType.Coin);
            AddItem(WeaponType.Hammer);
            AddItem(SkinType.Green);
            AddItem(HatType.None);
            AddItem(PantType.Green);

            SetItemSelected(WeaponType.Hammer);
            SetItemSelected(SkinType.Green);
            SetItemSelected(HatType.None);
            SetItemSelected(PantType.Green);
        }
    }

    public static Dictionary<T, Item<T>> GetAllItem<T>() where T : Enum
    {
        ItemCollection<T> itemCollection = GetCollection<T>();
        return itemCollection.GetDictItem();
    }

    public static bool CheckItemExis<T>(T itemType) where T : Enum
    {
        ItemCollection<T> itemCollection = GetCollection(itemType);
        return itemCollection.CheckItem(itemType);
    }

    public static Item<T> GetItem<T>(T type) where T : Enum
    {
        ItemCollection<T> itemCollection = GetCollection(type);
        return itemCollection.GetItem(type);
    }

    public static void AddItem<T>(T type) where T : Enum
    {
        Item<T> item = new(type);
        AddItem(item);
    }

    public static void AddItem<T>(Item<T> item) where T : Enum
    {
        ItemCollection<T> itemCollection = GetCollection<T>();
        itemCollection.AddItem(item);
    }

    public static void AddOrUpdateItem<T>(T type, int number) where T : Enum
    {
        AddOrUpdateItem(new Item<T>(type, number));
    }

    public static void UpdateItem<T>(T type, int number) where T : Enum
    {
        ItemCollection<T> itemCollection = GetCollection<T>();
        itemCollection.UpdateItem(new Item<T>(type, number));
    }

    public static bool RemoveItem<T>(T type) where T : Enum
    {
        ItemCollection<T> itemCollection = GetCollection<T>();
        return itemCollection.RemoveItem(type);
    }

    public static bool TryGetItemSelected<T>(out T type) where T : Enum
    {
        ItemCollection<T> itemCollection = GetCollection<T>();
        return itemCollection.TryGetItemSelected(out type);
    }

    public static void SetItemSelected<T>(T itemType) where T : Enum
    {
        ItemCollection<T> itemCollection = GetCollection<T>();
        itemCollection.SetItemSelected(itemType);
    }

    public static void ClearAll()
    {
        foreach(var item in dictItemCollection)
        {
            item.Value.OnClear();
        }
        Init();
    }

    private static void AddOrUpdateItem<T>(Item<T> item) where T : Enum
    {
        ItemCollection<T> itemCollection = GetCollection<T>();
        itemCollection.CreateOrUpdateItem(item);
    }

    private static ItemCollection<T> GetCollection<T>(T item) where T : Enum
    {
        return GetCollection<T>(item.GetType());
    }

    private static ItemCollection<T> GetCollection<T>() where T : Enum
    {
        return GetCollection<T>(typeof(T));
    }

    private static ItemCollection<T> GetCollection<T>(Type itemType) where T : Enum
    {
        ItemCollection<T> itemCollection;
        if (dictItemCollection.ContainsKey(itemType))
        {
            itemCollection = dictItemCollection[itemType] as ItemCollection<T>;
        }
        else
        {
            throw new Exception("Item Collection not valid.");
        }
        return itemCollection;
    }
}
