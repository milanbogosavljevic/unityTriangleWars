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
    private static int BRONZE_MEDAL_ENEMIES_KILLED = 150;
    private static int SILVER_MEDAL_ENEMIES_KILLED = 400;
    private static int GOLD_MEDAL_ENEMIES_KILLED = 950;
    private static SaveLoadSystem _saveLoadSystem;
    private static GameData _data;

    // void Awake()
    // {
    //     //_gameController = FindObjectOfType<GameController>();// mozda i ne treba
    //     RestoreHitMedalsStatus();
    // }

    public static void RestoreMedals()
    {
        _saveLoadSystem = GameObject.FindWithTag("SaveLoadSystem").GetComponent<SaveLoadSystem>();
        _data = _saveLoadSystem.GetGameData();
        RestoreHitMedalsStatus();
        RestoreEnemiesKilledMedalsStatus();
    }

    public static void RestoreHitMedalsStatus()
    {
        _bronzeHitsInRowMedalCollected = _data.bronzeHitsInRow;
        _silverHitsInRowMedalCollected = _data.silverHitsInRow;
        _goldHitsInRowMedalCollected = _data.goldHitsInRow;
    }

    public static void RestoreEnemiesKilledMedalsStatus()
    {
        _bronzeEnemiesKilledMedalCollected = _data.bronzeEnemiesKilled;
        _silverEnemiesKilledMedalCollected = _data.silverEnemiesKilled;
        _goldEnemiesKilledMedalCollected = _data.goldEnemiesKilled;
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
                _saveLoadSystem.SaveAchievement("bronzeHitsInRow");
            }
        }
        if(_hitCounter >= SILVER_MEDAL_HITS_IN_ROW)
        {
            if(!_silverHitsInRowMedalCollected)
            {
                _silverHitsInRowMedalCollected = true;
                _saveLoadSystem.SaveAchievement("silverHitsInRow");
            }
        }
        if(_hitCounter >= GOLD_MEDAL_HITS_IN_ROW)
        {
            if(!_goldHitsInRowMedalCollected)
            {
                _goldHitsInRowMedalCollected = true;
                _saveLoadSystem.SaveAchievement("goldHitsInRow");
            }
        }
    }

    public static void CheckEnemiesKilledMedals()
    {
        GameData _data = _saveLoadSystem.GetGameData();
        float enemiesKilled = _data.enemiesHit;
        if(enemiesKilled >= BRONZE_MEDAL_ENEMIES_KILLED)
        {
            if(!_bronzeEnemiesKilledMedalCollected)
            {
                _bronzeEnemiesKilledMedalCollected = true;
                _saveLoadSystem.SaveAchievement("bronzeEnemiesKilled");
            }
        }
        if(enemiesKilled >= SILVER_MEDAL_ENEMIES_KILLED)
        {
            if(!_silverEnemiesKilledMedalCollected)
            {
                _silverEnemiesKilledMedalCollected = true;
                _saveLoadSystem.SaveAchievement("silverEnemiesKilled");
            }
        }
        if(enemiesKilled >= GOLD_MEDAL_ENEMIES_KILLED)
        {
            if(!_goldEnemiesKilledMedalCollected)
            {
                _goldEnemiesKilledMedalCollected = true;
                _saveLoadSystem.SaveAchievement("goldEnemiesKilled");
            }
        }
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
