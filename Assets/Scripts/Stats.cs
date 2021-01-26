using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    private float _bulletsFired;
    private float _enemiesHit;
    private float _accuracy;

    public void RestoreStats()
    {
        _bulletsFired = PlayerPrefs.GetFloat("BulletsFired", 0);
        _enemiesHit = PlayerPrefs.GetFloat("EnemiesHit", 0);
        _accuracy = PlayerPrefs.GetFloat("Accuracy", 0f);
    }

    public void SaveStats()
    {
        _accuracy = (float)System.Math.Round(_enemiesHit / (_bulletsFired / 100f), 2);

        PlayerPrefs.SetFloat("BulletsFired", _bulletsFired);
        PlayerPrefs.SetFloat("EnemiesHit", _enemiesHit);
        PlayerPrefs.SetFloat("Accuracy", _accuracy);
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
