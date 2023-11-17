using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractSkinScripableObject<T> : ScriptableObject, IItemShop<T> where T : Enum
{
    [SerializeField] private T type;
    [SerializeField] private string skinName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int price;

    public string SkinName => skinName;
    public T Type => type;

    private Image image;

    public Image GetImage()
    {
        if (image == null)
        {
            GameObject imageObject = new("UIImage");
            imageObject.AddComponent<RectTransform>();
            imageObject.AddComponent<CanvasRenderer>();
            image = imageObject.AddComponent<Image>();
            image.sprite = sprite;
        }
        return image;
    }

    public T GetSkinType()
    {
        return type;
    }

    public int GetPrice()
    {
        return price;
    }
}
