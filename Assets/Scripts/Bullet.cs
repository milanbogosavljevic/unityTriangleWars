using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameController GameController;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private Player Player;
    //[SerializeField] private GameController _gameController;

    private bool _moveBullet = false;
    private float _maxUp;

    void Start()
    {
        _maxUp = Camera.main.orthographicSize;
    }

    void Update()
    {
        if (_moveBullet)
        {
            if(transform.position.y < _maxUp)
            {
                transform.position += Time.deltaTime * _bulletSpeed * Vector3.up;
            }
            else
            {
                StopMoving();
            }
        }
    }

/*    private void OnCollisionEnter2D(Collision2D collision)// collider ili bullet mora da bude dynamic
    {
        Debug.Log("omn enter");
        if (collision.gameObject.CompareTag("enemy"))
        {
            gameObject.SetActive(false);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            StopMoving();
            collision.gameObject.GetComponent<Enemy>().EnemyIsHit();
            GameController.PlayerHitsEnemy();// OVDE TREBA DA SE POSALJE KOG JE NEPRIJATELJA POGODIO DA BI ZNAO GC KOLIKO DA POMERI GHOST LINE
        }
    }

    public void StartMoving()
    {
        ResetPosition();
        _moveBullet = true;
    }

    public void StopMoving()
    {
        gameObject.SetActive(false);
        _moveBullet = false;
    }

    private void ResetPosition()
    {
        transform.position = Player.transform.position;
        gameObject.SetActive(true);
    }
}
