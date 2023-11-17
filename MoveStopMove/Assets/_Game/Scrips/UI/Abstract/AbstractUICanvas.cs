public abstract class AbstractUICanvas : AbstractUI
{
    public override void Open()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            OnShowUI();
        }
    }

    public override void Close()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            OnHiddenUI();
        }
    }

    public override void OnInit()
    {

    }

    public override void ReloadUI()
    {

    }

    protected virtual void OnShowUI()
    {

    }

    protected virtual void OnHiddenUI()
    {

    }
}
