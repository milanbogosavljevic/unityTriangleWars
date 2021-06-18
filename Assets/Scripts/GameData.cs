[System.Serializable]
public class GameData
{
    public int highScore = 0;
    public float bulletsFired = 0f;
    public float enemiesHit = 0f;
    public float accuracy = 0f;
    public bool bronzeHitsInRow = false;
    public bool silverHitsInRow = false;
    public bool goldHitsInRow = false;
    public bool bronzeEnemiesKilled = false;
    public bool silverEnemiesKilled = false;
    public bool goldEnemiesKilled = false;
    public bool dontShowInfo = false;
    public bool soundIsOn = true;
    public bool musicIsOn = true;
}
