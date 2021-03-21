using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyBullet EnemyBulletPrefab;
    [SerializeField] ParticleSystem ExplosionPrefab;

    private float _moveSpeed;
    private float _maxLeft;
    private float _maxRight;
    private float _shootingInterval;
    private bool _cahShoot;
    private bool _moveLeft = false;
    private bool _moveRight = false;
    private string _moveDirection;
    private float _enemyMoveLineBy;
    private int _points;
    private float _bulletSpeed;
    private EnemyBullet _bullet;
    private List<EnemyBullet> _bullets = new List<EnemyBullet>();
    private ParticleSystem _explosion;
    private bool _pingPongMove;
    private SoundController _soundController;

    void Awake()
    {
        _soundController = GameObject.FindWithTag("SoundController").GetComponent<SoundController>();

        _maxRight = GameBoundaries.RightBoundary;
        _maxLeft = GameBoundaries.LeftBoundary;

        _explosion = Instantiate(ExplosionPrefab);
        _explosion.gameObject.SetActive(false);
    }

    public void SetEnemyProperties(float yPositionFromTop, float moveSpeed, bool canShoot, float shootingInterval, float moveLineBy, float bulletSpeed, Sprite skin, string moveDirection, int points, bool pingPongMove, bool alphaAnimation)
    {
        _moveDirection = moveDirection;
        float x = _moveDirection == "right" ? _maxLeft - 1.5f : _maxRight + 1.5f;
        float y = GameBoundaries.UpBoundary - yPositionFromTop;
        transform.position = new Vector2(x,y);
        _moveSpeed = moveSpeed;
        _cahShoot = canShoot;
        _shootingInterval = shootingInterval;
        _enemyMoveLineBy = moveLineBy;
        _points = points;

        SetSkin(skin);
        StartMoving();

        if (_cahShoot)
        {
            _bulletSpeed = bulletSpeed;
            _bullet = Instantiate(EnemyBulletPrefab);
            _bullet.SetBulletSpeed(_bulletSpeed);
            _bullet.SetEnemy(this);
            _bullets.Add(_bullet);
            InvokeRepeating("FireBullet", 5f, _shootingInterval);
        }

        _pingPongMove = pingPongMove;

        if (!_pingPongMove)
        {
            _maxRight += 0.4f;
            _maxLeft -= 0.4f;
        }

        if (alphaAnimation)
        {
            LeanTween.alpha(gameObject, 0f, 2f).setLoopPingPong();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        if (_moveLeft)
        {
            if (transform.position.x > _maxLeft)
            {
                transform.position += Time.deltaTime * _moveSpeed * Vector3.left;
            }
            else
            {
                if (_pingPongMove)
                {
                    SwitchDirection();
                }
                else
                {
                    TeleportToOppositeSide();
                }
            }
        }
        else if (_moveRight)
        {
            if (transform.position.x < _maxRight)
            {
                transform.position += Time.deltaTime * _moveSpeed * Vector3.right;
            }
            else
            {
                if (_pingPongMove)
                {
                    SwitchDirection();
                }
                else
                {
                    TeleportToOppositeSide();
                }
            }
        }
    }

    void SwitchDirection()
    {
        _moveLeft = !_moveLeft;
        _moveRight = !_moveRight;
    }

    void TeleportToOppositeSide()
    {
        float x = _moveDirection == "right" ? _maxLeft : _maxRight;
        float y = transform.position.y;
        transform.position = new Vector2(x, y);
    }

    void StopMoving()
    {
        _moveLeft = _moveRight = false;
    }

    void ResetPosition()
    {
        _moveDirection = Random.value > 0.5f ? "left" : "right";
        float xPos = _moveDirection == "left" ? _maxRight + 1f : _maxLeft - 1f;
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        StartMoving();
    }

    void FireBullet()
    {
        bool foundBullet = false;

        for (int i = 0; i < _bullets.Count; i++)
        {
            if (!_bullets[i].gameObject.activeSelf)
            {
                _bullets[i].StartMoving();
                foundBullet = true;
                break;
            }
        }

        if (!foundBullet)
        {
            EnemyBullet newBullet = Instantiate(EnemyBulletPrefab);
            newBullet.SetBulletSpeed(_bulletSpeed);
            newBullet.SetEnemy(this);
            _bullets.Add(newBullet);
            newBullet.StartMoving();
            
        }

        _soundController.PlayEnemyShootSound();
    }

    public void StartMoving()
    {
        gameObject.SetActive(true);
        _moveLeft = _moveDirection == "left";
        _moveRight = !_moveLeft;
    }

    public void SetSkin(Sprite skin)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = skin;
    }

    public void EnemyIsHit()
    {
        _explosion.gameObject.GetComponent<ExplosionController>().ChangeParticleSkin(gameObject.GetComponent<SpriteRenderer>().sprite);
        _explosion.transform.position = transform.position;
        _explosion.gameObject.SetActive(true);
        _explosion.Play();
        gameObject.SetActive(false);
        StopMoving();
        ResetPosition();
    }

    public float GetEnemyMoveLineBy()
    {
        return _enemyMoveLineBy;
    }

    public int GetEnemyPoints()
    {
        return _points;
    }

    public void PauseMove(bool pause)
    {
        if (pause)
        {
            StopMoving();
        }
        else
        {
            StartMoving();
        }
    }
}
