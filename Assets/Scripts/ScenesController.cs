using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesController
{
    private static int THANKYOU_SCENE = 4;
    private static int GAME_SCENE = 3;
    private static int HOME_SCENE = 2;
    private static int INFO_SCENE = 1;
    public static void StartGame()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }

    public static void ShowHomeLevel()
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

    public static void ShowInfo()
    {
        SceneManager.LoadScene(INFO_SCENE);
    }

    public static void ShowThankYouScene()
    {
        SceneManager.LoadScene(THANKYOU_SCENE);
    }
}
