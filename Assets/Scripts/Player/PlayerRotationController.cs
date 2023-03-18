using System;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    [SerializeField]
    private float _rotationSmoothTime = 0.12f;
    private float _rotationVelocity;
    private Vector2 _rotationValue;
    private bool _canRotate = true;
    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_canRotate)
        {
            if (_rotationValue != Vector2.zero)
            {
                RotateAccordingToInputValue();
            }
        }
        else
        {
            RotateAccordingToCameraRotation();
        }
    }
    
    public void SetRotationDirection(Vector2 inputValue)
    {
        _rotationValue = inputValue; 
    }
    
    public void SetRotation(bool canRotate)
    {
        _canRotate = canRotate;
    }
    
    private void RotateAccordingToCameraRotation()
    {
        var yawCamera = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yawCamera, 0);
    }

    private void RotateAccordingToInputValue()
    {
        var yawCamera = _camera.transform.rotation.eulerAngles.y;
        
        var deltaAngle = Mathf.Atan2(_rotationValue.x, _rotationValue.y) * Mathf.Rad2Deg;

        var targetRotationAngle = deltaAngle + yawCamera;
        
        var rotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotationAngle,
            ref _rotationVelocity, _rotationSmoothTime);
        
        transform.rotation = Quaternion.Euler(0, rotationAngle, 0);
    }
}