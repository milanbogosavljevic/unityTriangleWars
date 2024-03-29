﻿using System.Collections;
using UnityEngine;

public class HelpItemsController : MonoBehaviour
{
    [SerializeField] GameObject[] HelpItems;
    private GameObject _selectedItem;
    private float _moveSpeed;
    private float _maxDown;
    private float _maxUp;
    private float _maxLeft;
    private float _maxRight;

    private int _releaseItemIntervalMin;
    private int _releaseItemIntervalMax;
    private IEnumerator  ReleaseItemCoroutine;

    private bool _moveItem = false;
    void Awake()
    {
        ReleaseItemCoroutine = ReleaseItem();
    }
    void Start()
    {
        _maxDown = GameBoundaries.DownBoundary - 1f;
        _maxUp = GameBoundaries.UpBoundary + 1f;
        _maxLeft = GameBoundaries.LeftBoundary;
        _maxRight = GameBoundaries.RightBoundary;

        _moveSpeed = 3.5f;
        _selectedItem = HelpItems[0];//puca ako se prvi nivo predje pre nego sto izbaci item

        //ReleaseItemCoroutine = ReleaseItem();
    }
    
    void Update()
    {
        if(_moveItem)
        {
            if (_selectedItem.transform.position.y > _maxDown)
            {
                _selectedItem.transform.position += Time.deltaTime * _moveSpeed * Vector3.down;
            }else
            {
                _selectedItem.SetActive(false);
                _moveItem = false;
                ActivateItem();
            }
        }
    }

    public void ActivateItem()
    {
        StopCoroutine(ReleaseItemCoroutine);
        ReleaseItemCoroutine = ReleaseItem();
        StartCoroutine(ReleaseItemCoroutine);
    }

    public void StopReleasingItems()
    {
        StopCoroutine(ReleaseItemCoroutine);
    }

    IEnumerator ReleaseItem()
    {
        int seconds = Random.Range(_releaseItemIntervalMin, _releaseItemIntervalMax);
        yield return new WaitForSeconds(seconds);
        int randomIndex = Random.Range(0, HelpItems.Length);
        _selectedItem = HelpItems[randomIndex];
        float x = Random.Range(_maxLeft, _maxRight);
        float y = _maxUp;
        _selectedItem.transform.position = new Vector2(x, y);
        _selectedItem.SetActive(true);
        _moveItem = true;
    }

    public void RemoveItem()
    {
        _moveItem = false;
        _selectedItem.SetActive(false);
    }

    public void ItemCollected()
    {
        RemoveItem();
        ActivateItem();
    }

    public void SetReleaseItemInterval(int[] intervalMinMax)
    {
        _releaseItemIntervalMin = intervalMinMax[0];
        _releaseItemIntervalMax = intervalMinMax[1];
    }
}
