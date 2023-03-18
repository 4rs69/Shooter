using System.Collections;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    [SerializeField] 
    private float _zoomValue = 10;
    [SerializeField] 
    private float _zoomChangeTime = 0.5f;
    [SerializeField]
    private Vector3 _targetCameraOffset = new(1, 0, -0.84f);
    
    private Vector3 _initialCameraOffset;
    private float _initialFieldOfView;
    private float _targetFieldOfView;
    private CinemachineFreeLook _freeLookCamera;
    private CinemachineCameraOffset _cameraOffset;

    private void Awake()
    {
        _freeLookCamera = GetComponent<CinemachineFreeLook>();
        _cameraOffset = GetComponent<CinemachineCameraOffset>();
        
        _initialFieldOfView = _freeLookCamera.m_Lens.FieldOfView;
        _targetFieldOfView = _initialFieldOfView - _zoomValue;
        _initialCameraOffset = _cameraOffset.m_Offset;
    }
    
    [UsedImplicitly]
    public void Zoom()
    {
        StartCoroutine(SetFieldOfView(_initialFieldOfView, _targetFieldOfView));
        StartCoroutine(SetOffset(_initialCameraOffset, _targetCameraOffset));
    }
    
    [UsedImplicitly]
    public void Unzoom()
    {
        StartCoroutine(SetFieldOfView(_targetFieldOfView, _initialFieldOfView));
        StartCoroutine(SetOffset(_targetCameraOffset, _initialCameraOffset));
    }
    
    private IEnumerator SetFieldOfView(float startFieldOfView, float targetFieldOfView)
    {
        var currentTime = 0f;

        while (currentTime < _zoomChangeTime)
        {
            _freeLookCamera.m_Lens.FieldOfView = Mathf.Lerp(
                startFieldOfView, targetFieldOfView, currentTime / _zoomChangeTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
        
        _freeLookCamera.m_Lens.FieldOfView = targetFieldOfView;
    }
    
    private IEnumerator SetOffset(Vector3 startOffset, Vector3 targetOffset)
    {
        var currentTime = 0f;

        while (currentTime < _zoomChangeTime)
        {
            _cameraOffset.m_Offset = Vector3.Lerp(
                startOffset, targetOffset, currentTime / _zoomChangeTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
        
        _cameraOffset.m_Offset = targetOffset;
    }
}
