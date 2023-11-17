using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private UIManager uiManager => UIManager.Instance;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;

        PlayerInventory.Init();

        uiManager.CloseAllPanel();
        uiManager.OpenUI<PanelMenu>();
    }
}
