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
    private CameraSizeController _cameraController;

    void Start()
    {
        _cameraController = Camera.main.GetComponent<CameraSizeController>();
        _currentLevel = 0;
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
        string[] startingMoveDirections = currentLevelInfo.GetEnemiesStartingDirection();
        
        for (int i = 0; i < numOfEnemies; i++)
        {
            Enemy enemy = Instantiate(EnemyPrefab);
            enemy.SetEnemyProperties(positions[i], moveSpeeds[i], canShoots[i], shootingIntervals[i], levels[i], bulletsSpeed[i], skins[i], startingMoveDirections[i]);
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

    private void GameOver()
    {
        Debug.Log("Game Over");
    }

    private void LevelPassed()
    {
        ClearEnemies();
        _currentLevel++;
        SetSceneForCurrentLevel();
    }

    private void ClearEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("enemyBullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }

        GameObject[] explosions = GameObject.FindGameObjectsWithTag("enemyExplosion");
        foreach (GameObject explosion in explosions)
        {
            Destroy(explosion);
        }
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
        PlayerLine.MoveLine(false);
        EnemyLine.MoveLine(false);
        if(lineName == "playerLine")
        {
            LevelPassed();
        }
        else
        {
            GameOver();
        }
    }

    public void PlayerHitsEnemy(int enemyLevel)
    {
        _cameraController.ShakeCamera();
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
