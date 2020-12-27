using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Bullet playerBullet;

    void Start()
    {
        
    }

    private void Awake()
    {

    }

    void Update()
    {
        
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
}
