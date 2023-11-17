using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupBuy : AbstractPopup
{
    [SerializeField] private EventTrigger buttonBuy;
    [SerializeField] private Button buttonClose;
    [SerializeField] private Text priceText;

    public event Action<ScriptableObject> OnBuy;

    private ScriptableObject itemScripableObject;

    protected override void OnOpenPopup()
    {
        buttonClose.onClick.AddListener(Close);
    }

    protected override void OnClosePopup()
    {
        buttonClose.onClick.RemoveAllListeners();
        buttonBuy.triggers.Clear();
        OnBuy = null;
    }

    public void SetItem<T>(ScriptableObject itemScripableObject) where T : Enum
    {
        if (itemScripableObject is IItemShop<T> itemShop)
        {
            this.itemScripableObject = itemScripableObject;
            SetTextPrice(itemShop.GetPrice());
            SetEventButtonBuy<T>();
        }
    }

    private void SetEventButtonBuy<T>() where T : Enum
    {
        buttonBuy.triggers.Clear();
        EventTrigger.Entry entry = new();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { Buy<T>(); });
        buttonBuy.triggers.Add(entry);
    }

    private void Buy<T>() where T : Enum
    {
        IItemShop<T> itemShop = itemScripableObject as IItemShop<T>;
        if (ShopManager.Instance.BuyItem(itemShop))
        {
            OnBuy?.Invoke(itemScripableObject);
            Close();
        }
    }

    private void SetTextPrice(int price)
    {
        priceText.text = price.ToString();
    }
}
