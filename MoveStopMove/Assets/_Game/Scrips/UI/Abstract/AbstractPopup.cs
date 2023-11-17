using System.Collections;
using UnityEngine;

public abstract class AbstractPopup : AbstractUI
{
    [SerializeField] private Transform panelPopup;

    private WaitForSeconds wait = new(0.01f);

    public override void OnInit()
    {

    }

    public override void Open()
    {
        if (!gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
            gameObject.SetActive(true);
            OnBeginOpen();
            StartCoroutine(IEUpScale());
        }
    }

    public override void Close()
    {
        if (gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
            OnClosePopup();
            StartCoroutine(IEDownScale());
        }
    }

    public override void ReloadUI()
    {

    }

    protected virtual void OnBeginOpen()
    {

    }

    protected virtual void OnOpenPopup()
    {

    }

    protected virtual void OnClosePopup()
    {

    }

    private IEnumerator IEUpScale()
    {
        float scale = 0;
        while (scale < 1)
        {
            scale += 0.1f;
            panelPopup.localScale = new Vector3(scale, scale, scale);
            yield return wait;
        }
        OnOpenPopup();
    }


    private IEnumerator IEDownScale()
    {
        float scale = panelPopup.localScale.x;
        while (scale > 0)
        {
            scale -= 0.1f;
            panelPopup.localScale = new Vector3(scale, scale, scale);
            yield return wait;
        }
        gameObject.SetActive(false);
    }
}
