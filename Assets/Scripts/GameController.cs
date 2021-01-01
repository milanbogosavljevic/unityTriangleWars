using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Bullet playerBullet;
    [SerializeField] LineController PlayerLine;
    [SerializeField] LineController PlayerGhostLine;
    [SerializeField] LineController EnemyLine;

    void Start()
    {
        EnemyLine.MoveLine(true);
        PlayerLine.MoveLine(true);
    }

    public void MovePlayer(string direction)
    {
        player.StartMoving(direction);
    }

    public void StopPlayer()
    {
        player.StopMoving();
    }

    public void PlayerFired()
    {
        playerBullet.StartMoving();
    }

    public void LineFinished(string lineName)
    {
        Debug.Log(lineName + " won");
        PlayerLine.MoveLine(false);
        EnemyLine.MoveLine(false);
    }

    public void PlayerHitsEnemy()
    {
        Vector3 ghostPosition = PlayerGhostLine.transform.position;
        float currentGhostPosition = ghostPosition.x;
        currentGhostPosition -= 1f;
        PlayerGhostLine.transform.position = new Vector3(currentGhostPosition, ghostPosition.y, ghostPosition.z);
        PlayerLine.MoveLine(true);
    }
}
/*
 TODO
    MORA DA POSTOJI BROJ KOJI CE SE SETOVATI NA POCETKU I KOJI CE ZAVISITI OD SIRINE EKRANA
    I ONDA TAJ BROJ DA SE MNOZI SA BROJEM KOJI DOBIJE PlayerHitsEnemy A KOJI CE ZAVISITI OD
    TOGA KOJI JE ENEMY POGODJEN.

    
 */
