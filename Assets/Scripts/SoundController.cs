using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource BackgroundMusic;
    [SerializeField] private AudioSource SoundsPlayer;

    [SerializeField] private AudioClip LevelPassed;
    [SerializeField] private AudioClip PlayerShoot;
    [SerializeField] private AudioClip EnemyShoot;
    [SerializeField] private AudioClip EnemyHit;
    [SerializeField] private AudioClip PlayerExplosion;
    [SerializeField] private AudioClip PlayerHit;
    [SerializeField] private AudioClip ItemCollected;
    [SerializeField] private AudioClip Reload;

    //[SerializeField] private AudioClip[] Sounds;

    private bool _musicIsOn;
    private bool _soundIsOn;
    private SaveLoadSystem _saveLoadSystem;
    private GameData _data;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SoundController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        _saveLoadSystem = GameObject.FindWithTag("SaveLoadSystem").GetComponent<SaveLoadSystem>();
        _data = _saveLoadSystem.GetGameData();

        _musicIsOn = _data.musicIsOn;
        _soundIsOn = _data.soundIsOn;
    }

    public void ToggleSound()
    {
        _soundIsOn = !_soundIsOn;
        _saveLoadSystem.SaveSoundState(_soundIsOn);
    }

    public void ToggleMusic()
    {
        _musicIsOn = !_musicIsOn;
        _saveLoadSystem.SaveMusicState(_musicIsOn);
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
            BackgroundMusic.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (_musicIsOn)
        {
            BackgroundMusic.Stop();
        }
    }

    public void PlayShootSound()
    {
        if (_soundIsOn)
        {
            SoundsPlayer.PlayOneShot(PlayerShoot, 1);
        }
    }

    public void PlayLevelPassedSound()
    {
        if (_soundIsOn)
        {
            SoundsPlayer.PlayOneShot(LevelPassed, 0.7f);
        }
    }

    public void PlayEnemyHitSound()
    {
        if (_soundIsOn)
        {
            SoundsPlayer.PlayOneShot(EnemyHit, 1);
        }
    }

    public void PlayPlayerExplosionSound()
    {
        if (_soundIsOn)
        {
            SoundsPlayer.PlayOneShot(PlayerExplosion, 1);
        }
    }

    public void PlayEnemyShootSound()
    {
        if (_soundIsOn)
        {
            SoundsPlayer.PlayOneShot(EnemyShoot, 1);
        }
    }

    public void PlayPlayerHitSound()
    {
        if (_soundIsOn)
        {
            SoundsPlayer.PlayOneShot(PlayerHit, 1);
        }
    }

    public void PlayItemCollectedSound()
    {
        if (_soundIsOn)
        {
            SoundsPlayer.PlayOneShot(ItemCollected, 1);
        }
    }

    public void PlayReloadSound()
    {
        if (_soundIsOn)
        {
            SoundsPlayer.PlayOneShot(Reload, 1);
        }
    }

/*    public void PlaySound(string soundName, float vol)
    {
        Debug.Log("play " + soundName);
        if (_soundIsOn)
        {
            for(int i = 0; i < Sounds.Length; i++)
            {
                Debug.Log(Sounds[i].name);
                if(Sounds[i].name == soundName)
                {
                    SoundsPlayer.PlayOneShot(Sounds[i], vol);
                }
            }
        }
    }*/
}
