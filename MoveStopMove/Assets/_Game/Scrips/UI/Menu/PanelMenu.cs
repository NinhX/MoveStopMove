using UnityEngine.SceneManagement;

public class PanelMenu : AbstractUICanvas
{
    public void PlayGame()
    {
        SceneManager.LoadScene(Constant.PLAY_SCENE_NAME);
    }
    public void ExitGame()
    {
        UIManager.Instance.OpenUI<PopupExit>();
    }

    public void ShowPanelSkin()
    {
        UIManager.Instance.CloseAllPanel();
        UIManager.Instance.OpenUI<UIShop>();
    }

    public void ShowPopUpSetting()
    {
        UIManager.Instance.OpenUI<PopupSetting>();
    }
}
