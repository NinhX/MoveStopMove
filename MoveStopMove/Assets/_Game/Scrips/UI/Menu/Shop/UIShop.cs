using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : AbstractUICanvas
{
    [SerializeField] private ScrollView[] scrolls;
    [SerializeField] private ScrollView scrollWeapon;
    [SerializeField] private Text coinText;
    [SerializeField] private Skin demoPref;

    private Skin demo;
    private Dictionary<Type, ScrollView> dictScroll = new();

    private void Awake()
    {
        for (int i = 0; i < scrolls.Length; i++)
        {
            ScrollView scroll = scrolls[i];
            dictScroll.Add(scroll.GetType(), scrolls[i]);
        }
    }

    public override void OnInit()
    {
        if (demo == null)
        {
            demo = Instantiate(demoPref, new(0, 0, 6), Quaternion.Euler(0, 180, 0));
        }

        scrollWeapon.OnInit(demo);
        for (int i = 0; i < scrolls.Length; i++)
        {
            scrolls[i].OnInit(demo);
        }
        ReloadUI();
    }

    public override void ReloadUI()
    {
        UpdateCoin();
    }

    protected override void OnShowUI()
    {
        StartCoroutine(IEStart());
        ShowScroll<SkinScroll>();
    }

    public void BackMenu()
    {
        UIManager.Instance.CloseAllPanel();
        UIManager.Instance.OpenUI<PanelMenu>();
    }

    public void ShowScroll(ScrollView scrollView)
    {
        HiddenAllScollView();
        scrollView.gameObject.SetActive(true);
    }

    public void ShowScroll<T>()
    {
        HiddenAllScollView();
        dictScroll[typeof(T)].gameObject.SetActive(true);
    }

    private void UpdateCoin()
    {
        int coin = PlayerInventory.GetItem(ItemType.Coin).number;
        coinText.text = coin.ToString();
    }

    private IEnumerator IEStart()
    {
        yield return new WaitForEndOfFrame();
        OnInit();
    }

    private void HiddenAllScollView()
    {
        foreach (var scroll in dictScroll)
        {
            scroll.Value.gameObject.SetActive(false);
        }
    }
    public bool BuyItem<T>(IItemShop<T> itemShop) where T : Enum
    {
        bool result = false;
        if (!PlayerInventory.CheckItemExis(itemShop.GetSkinType()))
        {
            int coinPlayer = PlayerInventory.GetItem(ItemType.Coin).number;
            int priceItem = itemShop.GetPrice();

            if (coinPlayer >= priceItem)
            {
                coinPlayer -= priceItem;
                PlayerInventory.AddOrUpdateItem(ItemType.Coin, coinPlayer);
                PlayerInventory.AddItem(itemShop.GetSkinType());
                result = true;
            }
        }
        return result;
    }
}
