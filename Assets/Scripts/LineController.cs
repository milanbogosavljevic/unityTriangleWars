using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] GameController GameController;
    [SerializeField] private float AddFromTop;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private LineController GhostLine;
    [SerializeField] private bool FollowGhost;

    private float _maxLeft;
    private float _maxRight;
    private float _maxUp;
    private bool _moveLine = false;
    public string Name;

    void Start()
    {
        _maxRight = GameBoundaries.RightBoundary;
        _maxLeft = GameBoundaries.LeftBoundary;
        _maxUp = GameBoundaries.UpBoundary;

        float yPos = _maxUp - AddFromTop;

        transform.position = new Vector3(_maxRight, yPos, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveLine)
        {
            if(transform.position.x > _maxLeft)
            {
                if (FollowGhost)
                {
                    if(transform.position.x > GhostLine.transform.position.x)
                    {
                        transform.position += Time.deltaTime * MoveSpeed * Vector3.left;
                    }
                    else
                    {
                        MoveLine(false);
                    }
                }
                else
                {
                    transform.position += Time.deltaTime * MoveSpeed * Vector3.left;
                }
            }
            else
            {
                GameController.LineFinished(Name);
            }
        }
    }

    public void MoveLine(bool move)
    {
        _moveLine = move;
    }

    public void ResetLinePosition()
    {
        transform.position = new Vector3(_maxRight, transform.position.y, transform.position.z);
    }

    public void CheckIfLineIsOverMax()
    {
        if(transform.position.x > _maxRight)
        {
            ResetLinePosition();
        }
    }

    public void SetLineMoveSpeed(float speed)
    {
        MoveSpeed = speed;
    }
}
