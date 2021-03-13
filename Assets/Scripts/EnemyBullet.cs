using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //[SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private Enemy Enemy;
    //[SerializeField] private GameController _gameController;

    private float _bulletSpeed;
    private bool _moveBullet = false;
    private float _maxDown;
    // Start is called before the first frame update
    void Awake()
    {
        _maxDown = GameBoundaries.DownBoundary;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveBullet)
        {
            if (transform.position.y > _maxDown)
            {
                transform.position += Time.deltaTime * _bulletSpeed * Vector3.down;
            }
            else
            {
                StopMoving();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            StopMoving();
            collision.gameObject.GetComponent<Player>().PlayerIsHit(Enemy.GetEnemyMoveLineBy());
        }
    }

    public void SetEnemy(Enemy enemy)
    {
        Enemy = enemy;
    }

    public void SetBulletSpeed(float speed)
    {
        _bulletSpeed = speed;
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
        transform.position = Enemy.transform.position;
        gameObject.SetActive(true);
    }
}
