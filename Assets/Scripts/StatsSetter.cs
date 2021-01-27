using TMPro;
using UnityEngine;

public class StatsSetter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Fired;
    [SerializeField] TextMeshProUGUI Hit;
    [SerializeField] TextMeshProUGUI Accuracy;

    void Start()
    {
        Fired.text = "BULLETS FIRED: " + PlayerPrefs.GetFloat("BulletsFired", 0);
        Hit.text = "ENEMIES HIT: " + PlayerPrefs.GetFloat("EnemiesHit", 0);
        Accuracy.text = "ACCURACY: " + PlayerPrefs.GetFloat("Accuracy", 0f) + "%";
    }
}
