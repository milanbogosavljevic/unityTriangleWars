
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
    
    void Start()
    {
        float soundAlpha = SoundController.IsSoundOn() ? 1 : 0.3f;
        Color soundColor = SoundButton.image.color;
        soundColor.a = soundAlpha;
        SoundButton.image.color = soundColor;

        float musicAlpha = SoundController.IsMusicOn() ? 1 : 0.3f;
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
        SoundController.ToggleSound();
        float a = SoundController.IsSoundOn() ? 1 : 0.3f;
        LeanTween.alpha(SoundButton.image.rectTransform, a, 0.5f).setEaseInOutQuint();
    }

    public void MusicButtonClickHandler()
    {
        SoundController.ToggleMusic();
        float a = SoundController.IsMusicOn() ? 1 : 0.3f;
        LeanTween.alpha(MusicButton.image.rectTransform, a, 0.5f).setEaseInOutQuint();
    }
}
