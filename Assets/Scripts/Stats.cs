using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    private float _bulletsFired;
    private float _enemiesHit;
    private float _accuracy;
    private int _highScore;
    private SaveLoadSystem _saveLoadSystem;
    GameData _data;

    public void RestoreStats()
    {
        _saveLoadSystem = GameObject.FindWithTag("SaveLoadSystem").GetComponent<SaveLoadSystem>();
        _data = _saveLoadSystem.GetGameData();

        _highScore = _data.highScore;
        _bulletsFired = _data.bulletsFired;
        _enemiesHit = _data.enemiesHit;
        _accuracy = _data.accuracy;
    
    }

    public void SaveStats()
    {
        _accuracy = (float)System.Math.Round(_enemiesHit / (_bulletsFired / 100f), 2);
        _saveLoadSystem.SaveStats(_bulletsFired, _enemiesHit, _accuracy);
    }

    public void CheckHighscore(int highscore)
    {
        if(highscore > _highScore)
        {
            _highScore = highscore;
            _saveLoadSystem.SaveScore(highscore);
        }
    }

    public void PlayerFired()
    {
        _bulletsFired++;
    }

    public void EnemyHit()
    {
        _enemiesHit++;
    }
}
