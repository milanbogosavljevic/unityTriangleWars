using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private float _maxDown;
    private float _meteorSpeed = 0f;
    private float angle = 0f;
    private float _rotationSpeed = 0f;
    GameController GameController;
    void Start()
    {
        GameController = FindObjectOfType<GameController>();
        _maxDown = GameBoundaries.DownBoundary - 1f;
    }

    
    void Update()
    {
        if (transform.position.y > _maxDown)
        {
            transform.position += Time.deltaTime * _meteorSpeed * Vector3.down;
            transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        }
        else
        {
            gameObject.SetActive(false);
            GameController.PlayerEscapesMeteor();
        }
    }

    public void SetMeteorSpeed(float speed)
    {
        _meteorSpeed = speed;
        _rotationSpeed = speed * 30f;
    }
}
