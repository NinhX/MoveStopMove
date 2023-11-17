using UnityEngine;
using UnityEngine.UI;

public class PopupExit : AbstractPopup
{
    [SerializeField] private Button buttonOk;
    [SerializeField] private Button buttonClose;

    protected override void OnOpenPopup()
    {
        buttonClose.onClick.AddListener(Close);
        buttonOk.onClick.AddListener(Exit);
    }

    protected override void OnClosePopup()
    {
        buttonClose.onClick.RemoveAllListeners();
        buttonOk.onClick.RemoveAllListeners();
    }

    private void Exit()
    {
        Application.Quit();
    }
}
