public class VictoryPanel : AbstractUICanvas
{
    public void NextLevel()
    {
        LevelManager.Instance.NextLevel();
    }

    public void BackMenu()
    {
        LevelManager.Instance.BackMenuScene();
    }
}
