using UnityEngine;
using UnityEngine.UI;

public class LosePanel : AbstractUICanvas
{
    [SerializeField] private Text textKiller;

    protected override void OnShowUI()
    {
        string killerName = MapManager.Instance.KillerName;
        if (killerName != null)
        {
            textKiller.text = killerName;
        }
    }

    public void BackMenu()
    {
        LevelManager.Instance.BackMenuScene();
    }

    public void PlayAgain()
    {
        GameManager.Instance.ChangeState(GameState.Start);
    }
}
