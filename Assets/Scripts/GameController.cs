using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] Player Player;
    [SerializeField] Bullet PlayerBullet;
    [SerializeField] LineController PlayerLine;
    [SerializeField] LineController PlayerGhostLine;
    [SerializeField] LineController EnemyLine;
    [SerializeField] private TextMeshProUGUI ScoreText;

    private float _ghostLineMovingMultiplier;
    private int _scoreMultiplier;
    private int _score;

    void Start()
    {
        _ghostLineMovingMultiplier = Camera.main.orthographicSize / 10;
        _scoreMultiplier = 50;
        _score = 0;
        EnemyLine.MoveLine(true);
        Player.SetAmmo(10);
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
        if (Player.PlayerCanFire())
        {
            PlayerBullet.StartMoving();
            Player.DecreaseAmmo();
        }
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
    KORIGOVATI EXPLODE ANIMACIJU
    NAMESTITI DA SE KORISTE RAZLICITE SLIKE ZA ANIMACIJU
    
 */
