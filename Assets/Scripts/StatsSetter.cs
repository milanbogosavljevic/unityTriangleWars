using TMPro;
using UnityEngine;

public class StatsSetter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Highscore;
    [SerializeField] TextMeshProUGUI Fired;
    [SerializeField] TextMeshProUGUI Hit;
    [SerializeField] TextMeshProUGUI Accuracy;
    private SaveLoadSystem _saveLoadSystem;

    void Start()
    {
        _saveLoadSystem = GameObject.FindWithTag("SaveLoadSystem").GetComponent<SaveLoadSystem>();
        GameData _data = _saveLoadSystem.GetGameData();

        Highscore.text = "HIGHSCORE: " + _data.highScore;
        Fired.text = "BULLETS FIRED: " + _data.bulletsFired;
        Hit.text = "ENEMIES HIT: " + _data.enemiesHit;
        Accuracy.text = "ACCURACY: " + _data.accuracy + "%";

        Achivements.RestoreMedals();
        //Achivements.RestoreHitMedalsStatus();
        //Achivements.RestoreEnemiesKilledMedalsStatus();
    }
}
