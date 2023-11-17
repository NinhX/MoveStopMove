using UnityEngine;
using UnityEngine.UI;

public class PlayPanel : AbstractUICanvas
{
    [SerializeField] private Text textAlive;
    [SerializeField] private Text textScore;

    private MapManager mapManager => MapManager.Instance;

    public override void ReloadUI()
    {
        textAlive.text = "Alive: " + mapManager.MaxBot.ToString();
        textScore.text = mapManager.PlayerScore.ToString();
    }

    public void Pause()
    {
        LevelManager.Instance.PauseGame();
    }
}
