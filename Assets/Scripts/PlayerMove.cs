using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _movingSpeed = 12f;

    [SerializeField] 
    private CharacterController _controller;
    

    private void Update()
    {
         var x = Input.GetAxis("Horizontal");
         var z = Input.GetAxis("Vertical");

         var move = transform.right * x + transform.forward * z;
       
         _controller.Move(move * _movingSpeed * Time.deltaTime);
         
    }

   
}
