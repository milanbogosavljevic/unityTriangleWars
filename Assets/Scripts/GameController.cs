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
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI PointsWon;

    private int _scoreMultiplier;
    private int _score;
    private int _currentLevel;
    private float _ghostLineMovingMultiplier;
    private float _maxRight;
    private float _maxLeft;
    private float _maxUp;
    private float _maxDown;

    private CameraSizeController _cameraController;

    void Start()
    {
        _maxRight = GameBoundaries.RightBoundary;
        _maxLeft = GameBoundaries.LeftBoundary;
        _maxUp = GameBoundaries.UpBoundary;
        _maxDown = GameBoundaries.DownBoundary;

        _cameraController = Camera.main.GetComponent<CameraSizeController>();
        _currentLevel = 1;
        _ghostLineMovingMultiplier = Camera.main.orthographicSize / 10;
        _scoreMultiplier = 50;
        _score = 0;
        SetSceneForCurrentLevel();

        Player.transform.position = new Vector3(0f, _maxDown + 1.7f, 0f);
    }

    private void SetSceneForCurrentLevel()
    {
        LevelsScriptableObj currentLevelInfo = LevelsInfo[_currentLevel];

        int numOfEnemies = currentLevelInfo.GetNumberOfEnemies();
        float enemyLineMoveSpeed = currentLevelInfo.GetEnemyLineMoveSpeed();
        int playerAmmo = currentLevelInfo.GetPlayerAmmo();

        float[] yPositionsFromTop = currentLevelInfo.GetEnemiesYPositionFromTop();
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
            enemy.SetEnemyProperties(yPositionsFromTop[i], moveSpeeds[i], canShoots[i], shootingIntervals[i], levels[i], bulletsSpeed[i], skins[i], startingMoveDirections[i]);
        }

        Player.SetAmmo(playerAmmo);

        EnemyLine.SetLineMoveSpeed(enemyLineMoveSpeed);
        EnemyLine.ResetLinePosition();
        PlayerGhostLine.ResetLinePosition();
        PlayerLine.ResetLinePosition();
        EnemyLine.MoveLine(true);
    }

    private void UpdateScore(int enemyLevel, Vector3 enemyPosition)
    {
        int scoreIncrementer = enemyLevel * _scoreMultiplier;
        _score += scoreIncrementer;
        ScoreText.text = _score.ToString();
        ShowPointsWon(scoreIncrementer, enemyPosition);
    }

    private void ShowPointsWon(int points, Vector3 enemyPosition)
    {
        PointsWon.gameObject.SetActive(true);
        PointsWon.text = "+" + points.ToString();

        float w = Screen.width / 2;
        float h = Screen.height / 2;

        float enemyX = enemyPosition.x;
        float enemyY = enemyPosition.y;

        float x;
        float y;

        if(enemyX < 0)
        {
            x = (enemyX / (_maxLeft / 100f)) * (w / 100);
            x *= -1;
        }
        else
        {
            x = (enemyX / (_maxRight / 100f)) * (w / 100);
        }

        if(enemyY < 0)
        {
            y = (enemyY / (_maxDown / 100f)) * (h / 100);
            y *= -1;
        }
        else
        {
            y = (enemyY / (_maxUp / 100f)) * (h / 100);
        }

        // ne kapiram bas zasto ovo mora ali radi
        x += w;
        y += h;

        PointsWon.transform.position = new Vector3(x, y, 0);

        LeanTween.moveY(PointsWon.gameObject, y + 150, 1f).setEaseLinear().setOnComplete(HidePointsWon);
        LeanTween.alpha(PointsWon.gameObject, 0f, 1f);
    }

    private void HidePointsWon()
    {
        PointsWon.gameObject.SetActive(false);
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

    public void PlayerHitsEnemy(int enemyLevel, Vector3 enemyPosition)
    {
        _cameraController.ShakeCamera();
        Vector3 ghostPosition = PlayerGhostLine.transform.position;
        float currentGhostPosition = ghostPosition.x;
        currentGhostPosition -= (_ghostLineMovingMultiplier * enemyLevel);
        PlayerGhostLine.transform.position = new Vector3(currentGhostPosition, ghostPosition.y, ghostPosition.z);
        PlayerLine.MoveLine(true);
        UpdateScore(enemyLevel, enemyPosition);
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
    SETOVATI POZICIJE PLAYERA I ENEMIA NA OSNOVU VELICINE EKRANA
 */
