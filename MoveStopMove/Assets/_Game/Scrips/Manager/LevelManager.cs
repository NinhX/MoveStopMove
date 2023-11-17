using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public int currentLevel { get; private set; }
    public bool isLoading { get; private set; }

    private GameManager gameManager => GameManager.Instance;
    private MapManager mapManager => MapManager.Instance;

    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel == mapManager.MapQty)
        {
            currentLevel = 0;
        }
        gameManager.ChangeState(GameState.Start);
    }

    public void LoadLevel()
    {
        StartCoroutine(IELoadLevel());
    }

    public void StartGame()
    {
        ChangeScene(Constant.PLAY_SCENE_NAME);
    }

    public void BackMenuScene()
    {
        ChangeScene(Constant.MENU_SCENE_NAME);
    }

    public void PauseGame()
    {
        if (gameManager.IsState(GameState.Play))
        {
            gameManager.ChangeState(GameState.Pause);
        }
    }

    public void ContinueGame()
    {
        if (gameManager.IsState(GameState.Pause))
        {
            gameManager.ChangeState(GameState.Play);
        }
    }

    private void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private IEnumerator IELoadLevel()
    {
        isLoading = true;
        mapManager.GenerateMap(currentLevel);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(1f);
        mapManager.AfterGenerateMap();
        isLoading = false;
    }
}
