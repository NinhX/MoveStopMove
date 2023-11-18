using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIShop : AbstractUICanvas
{
    [SerializeField] private ScrollView[] scrolls;
    [SerializeField] private ScrollView scrollWeapon;
    [SerializeField] private Text coinText;
    [SerializeField] private Demo demoPref;

    private Skin demoSkin;
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
        if (demoSkin == null)
        {
            Demo demo = Instantiate(demoPref);
            demoSkin = demo.Skin;
        }

        scrollWeapon.OnInit(demoSkin);
        for (int i = 0; i < scrolls.Length; i++)
        {
            scrolls[i].OnInit(demoSkin);
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

    public void ShowPopUpSetting()
    {
        UIManager.Instance.OpenUI<PopupSetting>();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(Constant.PLAY_SCENE_NAME);
    }
    public void ExitGame()
    {
        UIManager.Instance.OpenUI<PopupExit>();
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

    public void Toggle(GameObject shop)
    {
        shop.SetActive(!shop.activeInHierarchy);
    }

    public void Hidden(GameObject shop)
    {
        shop.SetActive(false);
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
}
