using UnityEngine;
using UnityEngine.UI;

public class ImagePoint : AbstractGameUnit
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Text textName;

    public void SetText(string text)
    {
        textName.text = text;
    }

    public void SetAnchoredPosition3D(Vector3 position)
    {
        rectTransform.anchoredPosition3D = position;
    }
}
