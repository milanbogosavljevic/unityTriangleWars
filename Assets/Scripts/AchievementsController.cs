using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementsController : MonoBehaviour
{

    [SerializeField] GameObject[] HitsInRowAchievements;
    [SerializeField] TextMeshProUGUI[] HitsInRowTextValues;

    [SerializeField] GameObject[] EnemiesKilledAchievements;
    [SerializeField] TextMeshProUGUI[] EnemiesKilledTextValues;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ScenesController.ShowHomeLevel();
        }
    }
    void Start()
    {
        SetHitsInRowAchievements();
        SetEnemiesKilledAchievements();
    }

    private void SetHitsInRowAchievements()
    {
        bool[] achievementsCollected = Achivements.GetHitsInRowAchievementsCollected();
        int[] achievementsValues = Achivements.GetHitsInRowAchievementsValues();
        for(int i = 0; i < HitsInRowAchievements.Length; i++)
        {
            Image achievement = HitsInRowAchievements[i].GetComponent<Image>();
            float achievementAlpha = achievementsCollected[i] ? 1f : 0.3f;
            achievement.color =  new Color(1f,1f,1f,achievementAlpha);

            HitsInRowTextValues[i].text = achievementsValues[i].ToString();
        }
    }

    private void SetEnemiesKilledAchievements()
    {
        bool[] achievementsCollected = Achivements.GetEnemiesKilledAchievementsCollected();
        int[] achievementsValues = Achivements.GetEnemiesKilledAchievementsValues();
        for(int i = 0; i < EnemiesKilledAchievements.Length; i++)
        {
            Image achievement = EnemiesKilledAchievements[i].GetComponent<Image>();
            float achievementAlpha = achievementsCollected[i] ? 1f : 0.3f;
            achievement.color =  new Color(1f,1f,1f,achievementAlpha);

            EnemiesKilledTextValues[i].text = achievementsValues[i].ToString();
        }
    }
}
