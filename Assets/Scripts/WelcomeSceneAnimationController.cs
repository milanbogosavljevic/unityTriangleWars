using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeSceneAnimationController : MonoBehaviour
{
    [SerializeField] private int NumOfImagesToAnimate;
    [SerializeField] private GameObject[] Images;

    private List<GameObject> _triangles = new List<GameObject>();

    private float _maxRight;
    private float _maxLeft;
    private float _maxUp;
    private float _maxDown;
    // Start is called before the first frame update
    void Start()
    {
        _maxRight = GameBoundaries.RightBoundary;
        _maxLeft = GameBoundaries.LeftBoundary;
        _maxUp = GameBoundaries.UpBoundary;
        _maxDown = GameBoundaries.DownBoundary;

        for (int i = 0; i < NumOfImagesToAnimate; i++)
        {
            var randomIndex = Random.Range(0, Images.Length);
            var image = Images[randomIndex];
            GameObject triangle = Instantiate(image);
            SetRandomPositionAndRotation(triangle);
            _triangles.Add(triangle);
        }

        InvokeRepeating("ChangePosition", 0f, 0.5f);
    }

    void ChangePosition()
    {
        var randomIndex = Random.Range(0, _triangles.Count);
        var triangle = _triangles[randomIndex];
        if (LeanTween.isTweening(triangle))
        {
            return;
        }
        LeanTween.alpha(triangle, 0.3f, 4f).setEaseInCubic().setOnComplete(()=> {
            LeanTween.alpha(triangle, 0f, 4f).setEaseOutCubic().setOnComplete(() => {
                SetRandomPositionAndRotation(triangle);
            });
        });
    }

    void SetRandomPositionAndRotation(GameObject triangle)
    {
        var randomPosition = new Vector2(Random.Range(_maxLeft, _maxRight), Random.Range(_maxUp, _maxDown));
        triangle.transform.position = randomPosition;
        triangle.transform.Rotate(0, 0, Random.Range(0, 360));
    }
}
