using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] LevelsScriptableObj[] LevelsInfo;
    [SerializeField] Player Player;
    [SerializeField] Enemy EnemyPrefab;
    [SerializeField] LineController PlayerLine;
    [SerializeField] LineController PlayerGhostLine;
    [SerializeField] LineController EnemyLine;
    [SerializeField] TextMeshProUGUI ScoreText;
    //[SerializeField] TextMeshProUGUI PointsWon;
    [SerializeField] TextMeshProUGUI LevelNumberLabel;
    [SerializeField] TextMeshProUGUI WellDoneLabel;

    [SerializeField] List<TextMeshProUGUI> PointsWonTextFields = new List<TextMeshProUGUI>();

    [SerializeField] GameObject Menu;
    [SerializeField] GameObject GameoverMenu;
    [SerializeField] Button ShootButton;
    [SerializeField] HelpItemsController HelpItemsController;
    [SerializeField] MeteorsController MeteorsController;

    private int _score;
    private int _currentLevel;
    private float _ghostLineMovingMultiplier;
    private float _maxRight;
    private float _maxLeft;
    private float _maxUp;
    private float _maxDown;

    private CameraSizeController _cameraController;
    private Stats _stats;
    private SoundController _soundController;

    //private Achivements _achievemnts;

    private int _numberOfLevels;

    private int _ammoToIncrease;
    private int _pauseLineFor;
    private int _pauseEnemiesFor;

    private bool _levelHasHelItems;
    private bool _levelHasMeteors;

    private IEnumerator  PauseEnemyLineCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu();
        }

        // ONLY DEV VERSION
        if(Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer("left");
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            StopPlayer("left");
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer("right");
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            StopPlayer("right");
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            PlayerFired();
        }
        // ONLY DEV VERSION
    }

    void Start()
    {
        GameoverMenu.transform.localScale = new Vector3(0,0,0);
        GameoverMenu.SetActive(false);

        //_soundController = GameObject.FindWithTag("SoundController").GetComponent<SoundController>();// problem kada se ne krene od pocetne scene
        _maxRight = GameBoundaries.RightBoundary;
        _maxLeft = GameBoundaries.LeftBoundary;
        _maxUp = GameBoundaries.UpBoundary;
        _maxDown = GameBoundaries.DownBoundary;

        _cameraController = Camera.main.GetComponent<CameraSizeController>();
        _currentLevel = 0;
        _ghostLineMovingMultiplier = Camera.main.orthographicSize / 10;
        _score = 0;
        SetSceneForCurrentLevel();

        Player.transform.position = new Vector3(0f, _maxDown + 1.7f, 0f);

        _stats = new Stats();
        _stats.RestoreStats();


        PauseEnemyLineCoroutine = PauseEnemyLine();

        //_achievemnts = new Achivements();

        //_soundController.PlayBackgroundMusic();
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
        float[] shootingIntervals = currentLevelInfo.GetEnemiesShootingInterval();
        float[] enemyMoveLineBy = currentLevelInfo.GetEnemiesMoveLineBy();
        float[] bulletsSpeed = currentLevelInfo.GetEnemiesBulletSpeed();
        Sprite[] skins = currentLevelInfo.GetEnemiesSkin();
        string[] startingMoveDirections = currentLevelInfo.GetEnemiesStartingDirection();
        int[] points = currentLevelInfo.GetEnemiesPoints();
        bool[] enemiesPingPongMove = currentLevelInfo.GetEnemiesPingPongMove();
        bool[] enemiesAlphaAnimation = currentLevelInfo.GetEnemiesAlphaAnimation();

        for (int i = 0; i < numOfEnemies; i++)
        {
            Enemy enemy = Instantiate(EnemyPrefab);
            enemy.SetEnemyProperties(yPositionsFromTop[i], moveSpeeds[i], canShoots[i], shootingIntervals[i], enemyMoveLineBy[i], bulletsSpeed[i], skins[i], startingMoveDirections[i], points[i], enemiesPingPongMove[i], enemiesAlphaAnimation[i]);
        }

        PauseEnemies(true);

        Player.SetAmmo(playerAmmo);

        EnemyLine.SetLineMoveSpeed(enemyLineMoveSpeed);
        EnemyLine.ResetLinePosition();
        PlayerGhostLine.ResetLinePosition();
        PlayerLine.ResetLinePosition();
        //EnemyLine.MoveLine(true);

        ShowLevelNumberAnimation();
        ShootButton.interactable = true;

        _numberOfLevels = LevelsInfo.Length;

        _levelHasHelItems = currentLevelInfo.HasHelpItems();

        if(_levelHasHelItems == true)
        {
            _ammoToIncrease = currentLevelInfo.GetPlayerAmmoToIncrease();
            _pauseLineFor = currentLevelInfo.GetPauseLineFor();
            _pauseEnemiesFor = currentLevelInfo.GetPauseEnemiesFor();

            HelpItemsController.SetReleaseItemInterval(currentLevelInfo.GetReleaseHelpItemsInterval());
            HelpItemsController.ActivateItem();
        }

        _levelHasMeteors = currentLevelInfo.HasMeteors();
        if(_levelHasMeteors)
        {
            float releaseInterval = currentLevelInfo.GetMeteorsReleaseInterval();
            float speed = currentLevelInfo.GetMeteorsSpeed();

            MeteorsController.gameObject.SetActive(true);
            MeteorsController.SetReleaseInterval(releaseInterval);
            MeteorsController.SetMeteorsMoveSpeed(speed);
            MeteorsController.SetMeteorMoveLineBy(currentLevelInfo.GetMeteorMoveLineBy());
            MeteorsController.SetMeteorPoints(currentLevelInfo.GetMeteorPoints());
            MeteorsController.StartReleasingMeteors();
        }
    }

    private void PauseEnemies(bool pause)
    {
        var enemies = FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in enemies)
        {
            enemy.PauseMove(pause);
        }
    }

    private void ShowLevelNumberAnimation()
    {
        float x = Screen.width * 1.5f * -1;
        float y = LevelNumberLabel.transform.position.y;
        LevelNumberLabel.transform.position = new Vector2(x, y);
        LevelNumberLabel.text = "Level " + (_currentLevel + 1).ToString();
        LevelNumberLabel.gameObject.SetActive(true);
        float animationDuration = 1f;
        float secondAnimationDelay = animationDuration + 0.3f;
        LeanTween.moveX(LevelNumberLabel.gameObject, Screen.width / 2, animationDuration).setEaseOutBack();
        LeanTween.moveX(LevelNumberLabel.gameObject, Screen.width * 1.5f, animationDuration).setEaseInBack().setDelay(secondAnimationDelay).setOnComplete(StartGame);
    }

    private void StartGame()
    {
        LevelNumberLabel.gameObject.SetActive(false);
        PauseEnemies(false);
        EnemyLine.MoveLine(true);
    }

    private void UpdateScore(int points)
    {
        _score += points;
        ScoreText.text = _score.ToString();
    }

    private TextMeshProUGUI GetPointsWonTextField()
    {
        for(int i = 0; i < PointsWonTextFields.Count; i++)
        {
            if (!PointsWonTextFields[i].gameObject.activeSelf)
            {
                return PointsWonTextFields[i];
            }
        }

        return PointsWonTextFields[0];
    }

    private void ShowPointsWon(int points, Vector3 enemyPosition)
    {
        TextMeshProUGUI PointsWon = GetPointsWonTextField();

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

        LeanTween.moveY(PointsWon.gameObject, y + 150, 1f).setEaseLinear().setOnComplete(()=>PointsWon.gameObject.SetActive(false));
    }

    private void GameOver()
    {
        GameoverMenu.SetActive(true);
        LeanTween.scale(GameoverMenu, new Vector3(1, 1, 1), 0.5f).setEaseInCirc();
        //GameoverMenu.transform.localScale = new Vector3(1, 1, 1);
        ShootButton.interactable = false;
        Player.ShowExplosion();
        //_soundController.PlayPlayerExplosionSound();
        _stats.SaveStats();
        _stats.CheckHighscore(_score);
    }

    private void LevelPassed()
    {
        //_soundController.PlayLevelPassedSound();
        ClearEnemies();
        ShowLevelPassedAnimation();
        _stats.SaveStats();
    }

    private void ShowLevelPassedAnimation()
    {
        float x = Screen.width * 1.5f * -1;
        float y = WellDoneLabel.transform.position.y;
        WellDoneLabel.transform.position = new Vector2(x, y);
        WellDoneLabel.gameObject.SetActive(true);
        float animationDuration = 1f;
        float secondAnimationDelay = animationDuration + 0.3f;
        LeanTween.moveX(WellDoneLabel.gameObject, Screen.width / 2, animationDuration).setEaseOutBack();
        LeanTween.moveX(WellDoneLabel.gameObject, Screen.width * 1.5f, animationDuration).setEaseInBack().setDelay(secondAnimationDelay).setOnComplete(LoadNexLevel);
    }

    private void LoadNexLevel()
    {
        WellDoneLabel.gameObject.SetActive(false);
        _currentLevel++;
        if(_currentLevel < _numberOfLevels)
        {
            SetSceneForCurrentLevel();
        }
        else
        {
            _stats.SaveStats();
            _stats.CheckHighscore(_score);
            ScenesController.ShowThankYouScene();
        }
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
        if (Player.PlayerCanFire())
        {
            //_soundController.PlayShootSound();
            Player.Fire();
            _stats.PlayerFired();
        }
        // else
        // {
        //     _stats.SaveStats();
        //     _stats.CheckHighscore(_score);
        // }
    }

    public void LineFinished(string lineName)
    {
        if(_levelHasHelItems == true)
        {
            HelpItemsController.RemoveItem();
            HelpItemsController.StopReleasingItems();
        }

        if(_levelHasMeteors)
        {
            MeteorsController.StopAndDeleteMeteors();
            MeteorsController.gameObject.SetActive(false);
        }
        
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

    public void PlayerHitsEnemy(float moveLineBy, Vector3 enemyPosition, int enemyPoints)
    {
        //_achievemnts.CountHit();
        Achivements.CountHit();
        _cameraController.ShakeCamera();
        //_soundController.PlayEnemyHitSound();

        Vector3 ghostPosition = PlayerGhostLine.transform.position;
        float currentGhostPosition = ghostPosition.x;
        currentGhostPosition -= (_ghostLineMovingMultiplier * moveLineBy);
        PlayerGhostLine.transform.position = new Vector3(currentGhostPosition, ghostPosition.y, ghostPosition.z);
        PlayerLine.MoveLine(true);

        UpdateScore(enemyPoints);
        ShowPointsWon(enemyPoints, enemyPosition);

        _stats.EnemyHit();
    }

    public void PlayerMissedEnemy()
    {
        //_achievemnts.ResetCountHit();
        Achivements.ResetCountHit();
    }

    public void PlayerEscapesMeteor()
    {
        Vector3 ghostPosition = PlayerGhostLine.transform.position;
        float currentGhostPosition = ghostPosition.x;
        currentGhostPosition -= (_ghostLineMovingMultiplier * MeteorsController.GetMeteorMoveLineBy());
        PlayerGhostLine.transform.position = new Vector3(currentGhostPosition, ghostPosition.y, ghostPosition.z);
        PlayerLine.MoveLine(true);

        UpdateScore(MeteorsController.GetMeteorPoints());
        //ShowPointsWon(MeteorsController.GetMeteorPoints(), new Vector3(0,0,0));
    }

    public void MeteorHitsPlayer(GameObject meteor)
    {
        meteor.GetComponent<Meteor>().Explode();
        float doubleMove = MeteorsController.GetMeteorMoveLineBy() * 3f;// mnozi se zato sto je previse lako
        EnemyHitsPlayer(doubleMove);
    }

    public void EnemyHitsPlayer(float moveLineBy)
    {
        _cameraController.ShakeCamera();
        //_soundController.PlayPlayerHitSound();

        Vector3 playerLinePosition = PlayerLine.transform.position;
        float linePositionX = playerLinePosition.x;
        linePositionX += (_ghostLineMovingMultiplier * moveLineBy);
        PlayerLine.transform.position = new Vector3(linePositionX, playerLinePosition.y, playerLinePosition.z);
        PlayerGhostLine.transform.position = new Vector3(linePositionX, playerLinePosition.y, playerLinePosition.z);

        PlayerLine.CheckIfLineIsOverMax();
        PlayerGhostLine.CheckIfLineIsOverMax();
    }

    public void ShowMenu()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        Menu.SetActive(!Menu.activeSelf);
        if (!Player.gameObject.activeSelf)
        {
            GameoverMenu.SetActive(!Menu.activeSelf);
        }
    }

    public void ExitToHome()
    {
        _stats.SaveStats();
        _stats.CheckHighscore(_score);
        //_soundController.StopBackgroundMusic();
        Time.timeScale = 1;
        Menu.SetActive(false);
        ScenesController.ShowHomeLevel();
    }

    public void RestartLevel()
    {
        _stats.SaveStats();
        _stats.CheckHighscore(_score);
        Time.timeScale = 1;
        Menu.SetActive(false);
        ScenesController.RestartLevel();
    }

    private void PlayerCollectedAmmo()
    {
        //_soundController.PlayReloadSound();
        Player.IncreaseAmmo(_ammoToIncrease);
    }

    private void PlayerCollectedPauseLine()
    {
        //_soundController.PlayItemCollectedSound();
        StopCoroutine(PauseEnemyLineCoroutine);
        PauseEnemyLineCoroutine = PauseEnemyLine();
        StartCoroutine(PauseEnemyLineCoroutine);
    }

    private void PlayerCollectedPauseEnemies()
    {
        //_soundController.PlayItemCollectedSound();
        StartCoroutine(PauseAndReleaseEnemiesMove());
    }

    public void PlayerCollectedItem(string itemType)
    {
        string methodName = "PlayerCollected" + itemType;
        HelpItemsController.ItemCollected();
        Invoke(methodName, 0f);
    }

    IEnumerator PauseAndReleaseEnemiesMove()
    {
        PauseEnemies(true);
        yield return new WaitForSeconds(_pauseEnemiesFor);
        PauseEnemies(false);
    }

    IEnumerator PauseEnemyLine()
    {
        EnemyLine.MoveLine(false);
        yield return new WaitForSeconds(_pauseLineFor);
        EnemyLine.MoveLine(true);
    }
}
/*
 TODO
    resiti bug sa setovanjem alfe

    bullets fired i enemies hit za achievements moze da se izvuce iz player prefs 
    napravio clasu i objekat achievements controller koja treba da manipulise alfom acivmenta
    difoltno da bude alpha npr 50 i da se kroz ovaj kontroller setuje naknadno

    napraviti glow
 */
