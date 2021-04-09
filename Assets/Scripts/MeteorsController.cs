using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorsController : MonoBehaviour
{
    private float _releaseInterval;
    [SerializeField] Meteor MeteorPrefab;
    private List<Meteor> _meteors = new List<Meteor>();
    private float _maxUp;
    private float _maxLeft;
    private float _maxRight;
    private float _meteorMoveSpeed;

    private float _meteorMoveLineBy;

    private int _meteorPoints;
    
    void Start()
    {
        _maxUp = GameBoundaries.UpBoundary + 1f;
        _maxLeft = GameBoundaries.LeftBoundary;
        _maxRight = GameBoundaries.RightBoundary; 
    }

    public void StopAndDeleteMeteors()
    {
        CancelInvoke();
        for(int i = 0; i < _meteors.Count; i++)
        {
            Destroy(_meteors[i].gameObject);
        }
    }

    public void SetReleaseInterval(float interval)
    {
        _releaseInterval = interval;
    }

    public void SetMeteorsMoveSpeed(float speed)
    {
        _meteorMoveSpeed = speed;
    }

    public void SetMeteorMoveLineBy(float moveBy)
    {
        _meteorMoveLineBy = moveBy;
    }

    public void SetMeteorPoints(int points)
    {
        _meteorPoints = points;
    }

    public float GetMeteorMoveLineBy()
    {
        return _meteorMoveLineBy;
    }

    public int GetMeteorPoints()
    {
        return _meteorPoints;
    }

    public void StartReleasingMeteors()
    {
        InvokeRepeating("ReleaseMeteor", 3f, _releaseInterval);
    }

    private void ReleaseMeteor()
    {
        bool foundMeteor = false;
        float x = Random.Range(_maxLeft, _maxRight);
        float y = _maxUp;
        float randomScale = Random.Range(0.3f, 0.6f);
        Vector3 scale = new Vector3(randomScale, randomScale, randomScale);
        float speed = _meteorMoveSpeed / randomScale;

        for(int i = 0; i < _meteors.Count; i++)
        {
            if(!_meteors[i].gameObject.activeSelf)
            {
                foundMeteor = true;
                
                _meteors[i].transform.position = new Vector2(x, y);
                _meteors[i].transform.localScale = scale;
                _meteors[i].SetMeteorSpeed(speed);
                _meteors[i].gameObject.SetActive(true);
                break;
            }
        }

        if(!foundMeteor)
        {
            Meteor meteor = Instantiate(MeteorPrefab);
            meteor.SetMeteorSpeed(speed);
            _meteors.Add(meteor);
            meteor.transform.position = new Vector2(x, y);
            meteor.transform.localScale = scale;
        }
    }
}
