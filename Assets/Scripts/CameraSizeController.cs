using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
    private float _targetAspect = 16f / 9f;
    private Camera _camera;
    private Transform _cameraTransform;
    private Vector3 _cameraOriginalPosition;

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        float currentAspect = (float)Screen.height / Screen.width;
        float difference;

        if (currentAspect != _targetAspect)
        {
            if (currentAspect > _targetAspect)
            {
                difference = currentAspect - _targetAspect;
                _camera.orthographicSize += (difference + 0.3f);
            }
            // else
            // {
            //     difference = _targetAspect - currentAspect;
            //     _camera.orthographicSize -= (difference + 0.3f);
            // }
        }
    }

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _cameraOriginalPosition = _cameraTransform.position;
    }

    private void ShakeCameraLeft()
    {
        LeanTween.moveX(_cameraTransform.gameObject, -0.1f, 0.01f).setOnComplete(ShakeCameraRight);
    }

    private void ShakeCameraRight()
    {
        LeanTween.moveX(_cameraTransform.gameObject, 0.1f, 0.01f);
    }

    private void ShakeCameraUp()
    {
        LeanTween.moveY(_cameraTransform.gameObject, 0.1f, 0.05f).setOnComplete(ShakeCameraDown);
    }

    private void ShakeCameraDown()
    {
        LeanTween.moveY(_cameraTransform.gameObject, -0.1f, 0.05f).setOnComplete(SetCameraOriginalPosition);
    }

    private void SetCameraOriginalPosition()
    {
        LeanTween.move(_cameraTransform.gameObject, _cameraOriginalPosition, 0.1f);
    }

    public void ShakeCamera()
    {
        ShakeCameraLeft();
        ShakeCameraUp();
    }
}
