using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    //[SerializeField] private AudioSource levelPassedSound;

    private bool _musicIsOn;
    private bool _soundIsOn;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MusicPlay"))
        {
            _musicIsOn = PlayerPrefs.GetString("MusicPlay") == "on";
        }
        else
        {
            _musicIsOn = true;
        }

        if (PlayerPrefs.HasKey("SoundPlay"))
        {
            _soundIsOn = PlayerPrefs.GetString("SoundPlay") == "on";
        }
        else
        {
            _soundIsOn = true;
        }


        DontDestroyOnLoad(this.gameObject);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SoundController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void ToggleSound()
    {
        _soundIsOn = !_soundIsOn;
        string onOff = _soundIsOn ? "on" : "off";
        PlayerPrefs.SetString("SoundPlay", onOff);
    }

    public void ToggleMusic()
    {
        _musicIsOn = !_musicIsOn;
        string onOff = _musicIsOn ? "on" : "off";
        PlayerPrefs.SetString("MusicPlay", onOff);
        if (_musicIsOn)
        {
            backgroundMusic.Play();
        }
        else
        {
            backgroundMusic.Pause();
        }
    }

    public bool IsMusicOn()
    {
        return _musicIsOn;
    }

    public bool IsSoundOn()
    {
        return _soundIsOn;
    }

    public void PlayBackgroundMusic()
    {
        if (_musicIsOn)
        {
            backgroundMusic.Play();
        }
    }

/*    public void PlayLevelPassedSound()
    {
        if (_soundIsOn)
        {
            levelPassedSound.Play();
        }
    }*/
}
