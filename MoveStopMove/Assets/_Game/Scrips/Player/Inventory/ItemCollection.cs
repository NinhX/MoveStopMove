using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemCollection<T> : AbstractCollection where T : Enum
{
    public List<Item<T>> listItem = new();
    public T itemSelected;

    private Dictionary<T, Item<T>> dictItem = new();
    private readonly string KEY;

    public ItemCollection (string KEY)
    {
        this.KEY = KEY;
        LoadDict();
    }

    public override void OnClear()
    {
        PlayerPrefs.DeleteKey(KEY);
        listItem.Clear();
        dictItem.Clear();
        itemSelected = default;
        AddItem(new Item<T>(itemSelected));
    }

    public void AddItem(Item<T> item)
    {
        if (!dictItem.ContainsKey(item.type))
        {
            dictItem.Add(item.type, item);
            listItem.Add(item);
        }
        SaveDict();
    }

    public void UpdateItem(Item<T> item)
    {
        if (dictItem.ContainsKey(item.type))
        {
            Item<T> itemOld = dictItem[item.type];
            itemOld.CopyFrom(item);
            SaveDict();
        }
    }

    public void CreateOrUpdateItem(Item<T> item)
    {
        if (!dictItem.ContainsKey(item.type))
        {
            AddItem(item);
        }
        else
        {
            UpdateItem(item);
        }
        SaveDict();
    }

    public Item<T> GetItem(T type)
    {
        Item<T> item = new(type);
        if (dictItem.ContainsKey(type))
        {
            item = dictItem[type].Clone();
        }
        return item;
    }

    public bool RemoveItem(T type)
    {
        bool result = false;
        if (dictItem.ContainsKey(type))
        {
            Item<T> item = dictItem[type];
            dictItem.Remove(type);
            listItem.Remove(item);
            SaveDict();
            result = true;
        }
        return result;
    }

    public bool CheckItem(T itemType)
    {
        return dictItem.ContainsKey(itemType);
    }

    public T GetItemSelected()
    {
        return itemSelected;
    }

    public void SetItemSelected(T typeItem)
    {
        if (dictItem.ContainsKey(typeItem))
        {
            itemSelected = typeItem;
            SaveDict();
        }
    }

    public Dictionary<T, Item<T>> GetDictItem()
    {
        Dictionary<T, Item<T>> newDict = new();
        foreach (var item in dictItem)
        {
            newDict.Add(item.Key, item.Value.Clone());
        }
        return newDict;
    }

    private void LoadDict()
    {
        string json = PlayerPrefs.GetString(KEY, "{}");
        JsonUtility.FromJsonOverwrite(json, this);
        dictItem.Clear();
        for (int i = 0; i < listItem.Count; i++)
        {
            Item<T> item = listItem[i];
            dictItem.Add(item.type, item);
        }
    }

    private string SaveDict()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(KEY, json);
        return json;
    }
}
