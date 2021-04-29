
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WelcomeSceneButtonsController : MonoBehaviour
{
    [SerializeField] Button PlayButton;
    [SerializeField] Button QuitButton;
    [SerializeField] Button SoundButton;
    [SerializeField] Button MusicButton;
    [SerializeField] SoundController SoundController;
    private SoundController _soundController;

    void Start()
    {
        _soundController = GameObject.FindWithTag("SoundController").GetComponent<SoundController>();

        float soundAlpha = _soundController.IsSoundOn() ? 1 : 0.3f;
        //float soundAlpha = true ? 1 : 0.3f;
        Color soundColor = SoundButton.image.color;
        soundColor.a = soundAlpha;
        SoundButton.image.color = soundColor;

        float musicAlpha = _soundController.IsMusicOn() ? 1 : 0.3f;
        //float musicAlpha = true ? 1 : 0.3f;
        Color musicColor = MusicButton.image.color;
        musicColor.a = musicAlpha;
        MusicButton.image.color = musicColor;
    }

    public void StartGame()
    {
        PlayButton.interactable = false;
        QuitButton.interactable = false;
        MusicButton.interactable = false;
        MusicButton.interactable = false;

        ScenesController.StartGame();
    }

    public void QuitGame()
    {
        ScenesController.QuitGame();
    }

    public void SoundButtonClickHandler()
    {
        _soundController.ToggleSound();
        float a = _soundController.IsSoundOn() ? 1 : 0.3f;
        LeanTween.alpha(SoundButton.image.rectTransform, a, 0.5f).setEaseInOutQuint();
    }

    public void MusicButtonClickHandler()
    {
        _soundController.ToggleMusic();
        float a = _soundController.IsMusicOn() ? 1 : 0.3f;
        LeanTween.alpha(MusicButton.image.rectTransform, a, 0.5f).setEaseInOutQuint();
    }

    public void AchievementsButtonClickHandler()
    {
        ScenesController.ShowAchievementsScene();
    }
}
