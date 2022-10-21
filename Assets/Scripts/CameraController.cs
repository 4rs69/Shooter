 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensivity = 100f;

    [SerializeField]
    private Transform _player;
    
    private float _mouseX;
    
    
    private 
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
       //движение мыши по х умноженная на чувствительность которую можно установить в юнити а также умножена
       //тайм дельта тайм что бы не зависить от текущей частоты кадров
       
        _mouseX = Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
        _player.Rotate(Vector3.up * _mouseX);

      
       

      
       
    }
}
