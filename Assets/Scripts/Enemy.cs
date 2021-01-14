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
    private int _enemyLevel;
    private EnemyBullet _bullet;
    private ParticleSystem _explosion;

    void Awake()
    {
        _maxRight = Camera.main.orthographicSize * Screen.width / Screen.height;
        _maxLeft = _maxRight * -1f;

        _bullet = Instantiate(EnemyBulletPrefab);
        _bullet.SetEnemy(this);

        _explosion = Instantiate(ExplosionPrefab);
        _explosion.gameObject.SetActive(false);
    }

    public void SetEnemyProperties(Vector2 position, float moveSpeed, bool canShoot, float shootingInterval, int enemyLevel, float bulletSpeed, Sprite skin)
    {
        transform.position = position;
        _moveSpeed = moveSpeed;
        _cahShoot = canShoot;
        _shootingInterval = shootingInterval;
        _enemyLevel = enemyLevel;

        _bullet.SetBulletSpeed(bulletSpeed);

        SetSkin(skin);
        StartMoving("right");

        if (_cahShoot)
        {
            InvokeRepeating("FireBullet", 2f, _shootingInterval);
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
                SwitchDirection();
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
                SwitchDirection();
            }
        }
    }

    void SwitchDirection()
    {
        _moveLeft = !_moveLeft;
        _moveRight = !_moveRight;
    }

    void StopMoving()
    {
        _moveLeft = _moveRight = false;
    }

    void ResetPosition()
    {
        string direction = Random.value > 0.5f ? "left" : "right";
        float xPos = direction == "left" ? _maxRight + 1f : _maxLeft - 1f;
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        StartMoving(direction);
    }

    void FireBullet()
    {
        _bullet.StartMoving();
    }

    public void StartMoving(string direction)
    {
        gameObject.SetActive(true);
        _moveLeft = direction == "left";
        _moveRight = !_moveLeft;
    }

    public void SetSkin(Sprite skin)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = skin;
    }

    public void EnemyIsHit()
    {
        //_explosion.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        //var textureSheetAnimation = _explosion.textureSheetAnimation;
        //textureSheetAnimation.AddSprite(test);
        _explosion.gameObject.GetComponent<ExplosionController>().ChangeParticleSkin(gameObject.GetComponent<SpriteRenderer>().sprite);
        _explosion.transform.position = transform.position;
        _explosion.gameObject.SetActive(true);
        _explosion.Play();
        gameObject.SetActive(false);
        StopMoving();
        ResetPosition();
    }

    public int GetEnemyLevel()
    {
        return _enemyLevel;
    }
}
