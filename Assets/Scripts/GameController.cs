using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] LevelsScriptableObj[] LevelsInfo;
    [SerializeField] Player Player;
    [SerializeField] Enemy EnemyPrefab;
    [SerializeField] LineController PlayerLine;
    [SerializeField] LineController PlayerGhostLine;
    [SerializeField] LineController EnemyLine;
    [SerializeField] private TextMeshProUGUI ScoreText;

    private float _ghostLineMovingMultiplier;
    private int _scoreMultiplier;
    private int _score;
    private int _currentLevel;

    void Start()
    {
        _currentLevel = 1;
        _ghostLineMovingMultiplier = Camera.main.orthographicSize / 10;
        _scoreMultiplier = 50;
        _score = 0;
        SetSceneForCurrentLevel();
    }

    private void SetSceneForCurrentLevel()
    {
        LevelsScriptableObj currentLevelInfo = LevelsInfo[_currentLevel];

        int numOfEnemies = currentLevelInfo.GetNumberOfEnemies();
        float enemyLineMoveSpeed = currentLevelInfo.GetEnemyLineMoveSpeed();
        int playerAmmo = currentLevelInfo.GetPlayerAmmo();

        Vector2[] positions = currentLevelInfo.GetEnemiesPosition();
        float[] moveSpeeds = currentLevelInfo.GetEnemiesMoveSpeed();
        bool[] canShoots = currentLevelInfo.GetEnemiesCanShoot();
        int[] shootingIntervals = currentLevelInfo.GetEnemiesShootingInterval();
        int[] levels = currentLevelInfo.GetEnemiesLevel();
        float[] bulletsSpeed = currentLevelInfo.GetEnemiesBulletSpeed();
        Sprite[] skins = currentLevelInfo.GetEnemiesSkin();
        
        for (int i = 0; i < numOfEnemies; i++)
        {
            Enemy enemy = Instantiate(EnemyPrefab);
            enemy.SetEnemyProperties(positions[i], moveSpeeds[i], canShoots[i], shootingIntervals[i], levels[i], bulletsSpeed[i], skins[i]);
        }

        Player.SetAmmo(playerAmmo);

        EnemyLine.SetLineMoveSpeed(enemyLineMoveSpeed);
        EnemyLine.ResetLinePosition();
        PlayerGhostLine.ResetLinePosition();
        PlayerLine.ResetLinePosition();
        EnemyLine.MoveLine(true);
    }

    private void UpdateScore(int enemyLevel)
    {
        _score += enemyLevel * _scoreMultiplier;
        ScoreText.text = _score.ToString();
    }

    public void MovePlayer(string direction)
    {
        Player.StartMoving(direction);
    }

    public void StopPlayer(string direction)
    {
        Player.StopMoving(direction);
    }

    public void PlayerFired()
    {
        Player.Fire();
    }

    public void LineFinished(string lineName)
    {
        Debug.Log(lineName + " won");
        PlayerLine.MoveLine(false);
        EnemyLine.MoveLine(false);
    }

    public void PlayerHitsEnemy(int enemyLevel)
    {
        Vector3 ghostPosition = PlayerGhostLine.transform.position;
        float currentGhostPosition = ghostPosition.x;
        currentGhostPosition -= (_ghostLineMovingMultiplier * enemyLevel);
        PlayerGhostLine.transform.position = new Vector3(currentGhostPosition, ghostPosition.y, ghostPosition.z);
        PlayerLine.MoveLine(true);
        UpdateScore(enemyLevel);
    }

    public void EnemyHitsPlayer(int enemyLevel)
    {
        Vector3 playerLinePosition = PlayerLine.transform.position;
        float linePositionX = playerLinePosition.x;
        linePositionX += (_ghostLineMovingMultiplier * enemyLevel);
        PlayerLine.transform.position = new Vector3(linePositionX, playerLinePosition.y, playerLinePosition.z);
        PlayerGhostLine.transform.position = new Vector3(linePositionX, playerLinePosition.y, playerLinePosition.z);

        PlayerLine.CheckIfLineIsOverMax();
        PlayerGhostLine.CheckIfLineIsOverMax();
    }
}
/*
 TODO
    
 */
