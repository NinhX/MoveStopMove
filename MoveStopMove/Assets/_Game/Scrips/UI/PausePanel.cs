using UnityEngine;

public class PausePanel : AbstractUICanvas
{
    public void BackMenu()
    {
        LevelManager.Instance.BackMenuScene();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        LevelManager.Instance.ContinueGame();
    }
}
