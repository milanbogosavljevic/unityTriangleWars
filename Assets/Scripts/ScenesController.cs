using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesController
{
    private static int GAME_SCENE = 2;
    private static int HOME_SCENE = 1;
    public static void StartGame()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }

    public static void ExitLevel()
    {
        SceneManager.LoadScene(HOME_SCENE);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void RestartLevel()
    {
        StartGame();
    }
}
