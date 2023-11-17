using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{
    [SerializeField] protected GameObject imgLockPrefab;
    [SerializeField] private RectTransform bgPrefab;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Text textName;
    [SerializeField] private Vector2 sizeContent;
    [SerializeField] private Transform imgSelected;

    protected Dictionary<ScriptableObject, GameObject> dictLockImg = new();
    protected Skin demo;
    private Dictionary<Enum, RectTransform> contents = new();
    private float widthScoll;

    public virtual void OnInit(Skin demo)
    {
        this.demo = demo;
        RectTransform scrollRectTransform = scrollRect.GetComponent<RectTransform>();
        widthScoll = scrollRectTransform.sizeDelta.x;
    }

    public void AddContent<T>(T index, RectTransform content) where T : Enum
    {
        content.sizeDelta = sizeContent;
        content.SetParent(scrollRect.content);
        content.localScale = Vector3.one;
        contents.Add(index, content);
    }

    public void SelectContent(Enum index)
    {
        if (contents.ContainsKey(index))
        {
            RectTransform contentSelected = contents[index];
            UpdateScoll(contentSelected);
        }
    }

    public void SetTextName(string text)
    {
        if (textName != null)
        {
            textName.text = text;
        }
    }

    public void ResetContent()
    {
        contents.Clear();
    }

    private void UpdateScoll(RectTransform contentSelected)
    {
        RectTransform contentLast = contents.Last().Value;

        float widthContentSelected = contentSelected.anchoredPosition.x;
        float widthContentLast = contentLast.anchoredPosition.x;
        float halfWidthScroll = widthScoll / 2;

        // update scroll bar
        if (widthContentSelected <= halfWidthScroll)
        {
            widthContentSelected = 0;
        }
        if (widthContentSelected >= widthContentLast + sizeContent.x / 2 - halfWidthScroll)
        {
            widthContentSelected = widthContentLast;
        }
        float percentScollBar = widthContentSelected / widthContentLast;
        scrollRect.horizontalNormalizedPosition = percentScollBar;

        if (imgSelected != null)
        {
            imgSelected.gameObject.SetActive(true);
            imgSelected.position = contentSelected.position;
            imgSelected.SetParent(contentSelected);
            imgSelected.SetSiblingIndex(0);
        }
    }

    protected void AddContentScroll<T>(Dictionary<T, AbstractSkinScripableObject<T>> dictSkin, Action<T> callback) where T : Enum
    {
        ResetContent();
        foreach (var skin in dictSkin)
        {
            AbstractSkinScripableObject<T> skinValue = skin.Value;
            RectTransform bg = Instantiate(bgPrefab);
            skinValue.GetImage().transform.SetParent(bg);
            if (!PlayerInventory.CheckItemExis(skinValue.Type))
            {
                dictLockImg.Add(skinValue, Instantiate(imgLockPrefab, bg));
            }

            AddTrigger(skinValue, callback);
            AddContent(skinValue.GetSkinType(), bg);
        }
    }

    protected void AddTrigger<T>(AbstractSkinScripableObject<T> skinScripableObject, Action<T> callback) where T : Enum
    {
        Image img = skinScripableObject.GetImage();
        EventTrigger trigger = img.GetOrAddComponent<EventTrigger>();
        if (trigger.triggers.Count == 0)
        {
            EventTrigger.Entry entry = new();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { callback(skinScripableObject.GetSkinType()); });
            trigger.triggers.Add(entry);
        }
    }

    protected PopupBuy OpenPopup<T>(AbstractSkinScripableObject<T> skinScripable) where T : Enum
    {
        PopupBuy popupBuy = UIManager.Instance.LoadUI<PopupBuy>();
        popupBuy.SetItem<T>(skinScripable);
        popupBuy.OnBuy += UpdateScroll;
        popupBuy.Open();
        return popupBuy;
    }

    private void UpdateScroll(ScriptableObject scriptableObject)
    {
        dictLockImg.Remove(scriptableObject, out GameObject imgBlock);
        Destroy(imgBlock);
    }
}
