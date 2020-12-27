using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _cahShoot;
    [SerializeField] private float _shootingInterval;
    [SerializeField] EnemyBullet EnemyBullet;
    private bool _moveLeft = false;
    private bool _moveRight = false;
    private float _maxLeft;
    private float _maxRight;

    void Start()
    {
        _maxRight = Camera.main.orthographicSize * Screen.width / Screen.height;
        _maxLeft = _maxRight * -1f;
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
        EnemyBullet.StartMoving();
    }

    public void StartMoving(string direction)
    {
        gameObject.SetActive(true);
        _moveLeft = direction == "left";
        _moveRight = !_moveLeft;
    }

    public void EnemyIsHit()
    {
        gameObject.SetActive(false);
        StopMoving();
        ResetPosition();
    }
}
