﻿using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] GameController GameController;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private TextMeshProUGUI AmmoText;
    [SerializeField] Bullet PlayerBullet;
    [SerializeField] ParticleSystem ExplosionPrefab;

    private bool _moveLeft = false;
    private bool _moveRight = false;
    private float _maxLeft;
    private float _maxRight;
    private int _ammo;

    void Start()
    {
        _maxRight = GameBoundaries.RightBoundary;
        _maxLeft = GameBoundaries.LeftBoundary;
    }

    private void Awake()
    {
        _moveSpeed = 3f;
    }

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
        if (_moveRight)
        {
            if (transform.position.x < _maxRight)
            {
                transform.position += Time.deltaTime * _moveSpeed * Vector3.right;
            }
        }
    }

    private void UpdateAmmoText()
    {
        AmmoText.text = _ammo.ToString();
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

    public void StopMoving(string direction)
    {
        //_moveLeft = _moveRight = false;
        if (direction == "left")
        {
            _moveLeft = false;
        }
        else
        {
            _moveRight = false;
        }
    }

    public void PlayerIsHit(float enemyLevel)
    {
        GameController.EnemyHitsPlayer(enemyLevel);
    }

    public void SetAmmo(int ammo)
    {
        _ammo = ammo;
        UpdateAmmoText();
    }

    public void DecreaseAmmo()
    {
        _ammo--;
        UpdateAmmoText();
    }

    public bool PlayerCanFire()
    {
        return _ammo > 0 && !PlayerBullet.isActiveAndEnabled;
    }

    public void Fire()
    {
        if (PlayerCanFire())
        {
            PlayerBullet.StartMoving();
            DecreaseAmmo();
        }
    }

    public void ShowExplosion()
    {
        gameObject.SetActive(false);
        var explosion = Instantiate(ExplosionPrefab);
        explosion.gameObject.GetComponent<ExplosionController>().ChangeParticleSkin(gameObject.GetComponent<SpriteRenderer>().sprite);
        explosion.transform.position = transform.position;
        explosion.Play();
        var main = explosion.main;
        main.stopAction = ParticleSystemStopAction.Destroy;
    }
}
