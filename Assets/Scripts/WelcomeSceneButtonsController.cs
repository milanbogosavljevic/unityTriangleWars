using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeSceneButtonsController : MonoBehaviour
{
    [SerializeField] Button SoundButton;
    [SerializeField] Button MusicButton;
    private bool _musicIsOn;
    private bool _soundIsOn;
    
    void Start()
    {
        _musicIsOn = true;
        _soundIsOn = true;
    }

    public void StartGame()
    {

    }

    public void QuitGame()
    {

    }

    public void SoundButtonClickHandler()
    {
        _soundIsOn = !_soundIsOn;
        float a = _soundIsOn ? 1 : 0.3f;
        LeanTween.alpha(SoundButton.image.rectTransform, a, 0.5f).setEaseInOutQuint();
    }

    public void MusicButtonClickHandler()
    {
        _musicIsOn = !_musicIsOn;
        float a = _musicIsOn ? 1 : 0.3f;
        LeanTween.alpha(MusicButton.image.rectTransform, a, 0.5f).setEaseInOutQuint();
    }
}
