using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Achivements
{
    //private GameController _gameController;
    private static bool _bronzeHitsInRowMedalCollected = false;
    private static bool _silverHitsInRowMedalCollected = false;
    private static bool _goldHitsInRowMedalCollected = false;
    private static int BRONZE_MEDAL_HITS_IN_ROW = 5;
    private static int SILVER_MEDAL_HITS_IN_ROW = 15;
    private static int GOLD_MEDAL_HITS_IN_ROW = 25;
    private static int _hitCounter = 0;

    private static bool _bronzeEnemiesKilledMedalCollected = false;
    private static bool _silverEnemiesKilledMedalCollected = false;
    private static bool _goldEnemiesKilledMedalCollected = false;
    private static int BRONZE_MEDAL_ENEMIES_KILLED = 40;//150
    private static int SILVER_MEDAL_ENEMIES_KILLED = 50;//400
    private static int GOLD_MEDAL_ENEMIES_KILLED = 60;//950

    //PlayerPrefs.GetFloat("EnemiesHit", 0); OVAKO DOCI DO CIFRE
    // ZA SAD SU SAMO SETOVANE KONSTANTE

    // void Awake()
    // {
    //     //_gameController = FindObjectOfType<GameController>();// mozda i ne treba
    //     RestoreHitMedalsStatus();
    // }

    public static void RestoreHitMedalsStatus()
    {
        _bronzeHitsInRowMedalCollected = PlayerPrefs.HasKey("BronzeHitsInRowMedal");
        _silverHitsInRowMedalCollected = PlayerPrefs.HasKey("SilverHitsInRowMedal");
        _goldHitsInRowMedalCollected = PlayerPrefs.HasKey("GoldHitsInRowMedal");
    }

    public static void RestoreEnemiesKilledMedalsStatus()
    {
        _bronzeEnemiesKilledMedalCollected = PlayerPrefs.HasKey("BronzeEnemiesKilledMedal");
        _silverEnemiesKilledMedalCollected = PlayerPrefs.HasKey("SilverEnemiesKilledMedal");
        _goldEnemiesKilledMedalCollected = PlayerPrefs.HasKey("GoldEnemiesKilledMedal");
        CheckEnemiesKilledMedals();
    }

    public static void CountHit()
    {
        if(!_goldHitsInRowMedalCollected)
        {
            _hitCounter++;
            CheckHitMedals();
        }
    }

    public static void ResetCountHit()
    {
        if(!_goldHitsInRowMedalCollected)
        {
            _hitCounter = 0;
        }
    }

    private static void CheckHitMedals()
    {
        if(_hitCounter >= BRONZE_MEDAL_HITS_IN_ROW)
        {
            if(!_bronzeHitsInRowMedalCollected)
            {
                _bronzeHitsInRowMedalCollected = true;
                StoreAchievemt("BronzeHitsInRowMedal");
            }
        }
        if(_hitCounter >= SILVER_MEDAL_HITS_IN_ROW)
        {
            if(!_silverHitsInRowMedalCollected)
            {
                _silverHitsInRowMedalCollected = true;
                StoreAchievemt("SilverHitsInRowMedal");
            }
        }
        if(_hitCounter >= GOLD_MEDAL_HITS_IN_ROW)
        {
            if(!_goldHitsInRowMedalCollected)
            {
                _goldHitsInRowMedalCollected = true;
                StoreAchievemt("GoldHitsInRowMedal");
            }
        }
    }

    public static void CheckEnemiesKilledMedals()
    {
        float enemiesKilled = PlayerPrefs.GetFloat("EnemiesHit", 0);
        if(enemiesKilled >= BRONZE_MEDAL_ENEMIES_KILLED)
        {
            if(!_bronzeEnemiesKilledMedalCollected)
            {
                _bronzeEnemiesKilledMedalCollected = true;
                StoreAchievemt("BronzeEnemiesKilledMedal");
            }
        }
        if(enemiesKilled >= SILVER_MEDAL_ENEMIES_KILLED)
        {
            if(!_silverEnemiesKilledMedalCollected)
            {
                _silverEnemiesKilledMedalCollected = true;
                StoreAchievemt("SilverEnemiesKilledMedal");
            }
        }
        if(enemiesKilled >= GOLD_MEDAL_ENEMIES_KILLED)
        {
            if(!_goldEnemiesKilledMedalCollected)
            {
                _goldEnemiesKilledMedalCollected = true;
                StoreAchievemt("GoldEnemiesKilledMedal");
            }
        }
    }

    private static void StoreAchievemt(string AchievemtCollected)
    {
        Debug.Log(AchievemtCollected);
        PlayerPrefs.SetInt(AchievemtCollected, 1);
    }

    public static bool[] GetHitsInRowAchievementsCollected()
    {
        bool[] values = new bool[]{_bronzeHitsInRowMedalCollected, _silverHitsInRowMedalCollected, _goldHitsInRowMedalCollected};
        return values;
    }

    public static int[] GetHitsInRowAchievementsValues()
    {
        int[] values = new int[]{BRONZE_MEDAL_HITS_IN_ROW, SILVER_MEDAL_HITS_IN_ROW, GOLD_MEDAL_HITS_IN_ROW};
        return values;
    }

    public static bool[] GetEnemiesKilledAchievementsCollected()
    {
        bool[] values = new bool[]{_bronzeEnemiesKilledMedalCollected, _silverEnemiesKilledMedalCollected, _goldEnemiesKilledMedalCollected};
        return values;
    }

    public static int[] GetEnemiesKilledAchievementsValues()
    {
        int[] values = new int[]{BRONZE_MEDAL_ENEMIES_KILLED, SILVER_MEDAL_ENEMIES_KILLED, GOLD_MEDAL_ENEMIES_KILLED};
        return values;
    }
}
