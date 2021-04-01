using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private float _maxDown;
    private float _meteorSpeed = 0f;
    private float angle = 0f;
    private float _rotationSpeed = 0.5f;
    void Start()
    {
        _maxDown = GameBoundaries.DownBoundary - 1f;
    }

    
    void Update()
    {
        if (transform.position.y > _maxDown)
        {
            transform.position += Time.deltaTime * _meteorSpeed * Vector3.down;
            angle += Time.deltaTime * _meteorSpeed;
            if(angle > 360f)
            {
                angle = 0f;
            }
            //transform.Rotate(0, 0, angle);
            transform.localEulerAngles = new Vector3(0,0,angle);
            Debug.Log("update " + angle);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetMeteorSpeed(float speed)
    {
        _meteorSpeed = speed;
        //_rotationSpeed = speed * 0.05f;
    }
}
