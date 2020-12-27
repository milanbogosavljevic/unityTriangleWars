using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    //[SerializeField] private GameController _gameController;
    //[SerializeField] private float _moveForce = 0f;
    private bool _moveLeft = false;
    private bool _moveRight = false;
    private float _maxLeft;
    private float _maxRight;

    //private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _maxRight = Camera.main.orthographicSize * Screen.width / Screen.height;
        _maxLeft = _maxRight * -1f;
    }

    private void Awake()
    {
        //_rb = GetComponent<Rigidbody2D>();
        _moveSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (_moveLeft)
        {
            if (transform.position.x > _maxLeft)
            {
                transform.position += Time.deltaTime * _moveSpeed * Vector3.left;
            }
        }
        else if (_moveRight)
        {
            if (transform.position.x < _maxRight)
            {
                transform.position += Time.deltaTime * _moveSpeed * Vector3.right;
            }
        }
    }

    public void StartMoving(string direction)
    {
        if(direction == "left")
        {
            _moveLeft = true;
        }
        else
        {
            _moveRight = true;
        }
    }

    public void StopMoving()
    {
        _moveLeft = _moveRight = false;
    }

    public void PlayerIsHit()
    {
        Debug.Log("player is hit");
    }

    /*    private void FixedUpdate()
    {
        if (_moveLeft)
        {
            _rb.AddForce(_moveForce * Vector3.left);
        }
        else if (_moveRight)
        {
            _rb.AddForce(_moveForce * Vector3.right);
        }
    }*/
}
