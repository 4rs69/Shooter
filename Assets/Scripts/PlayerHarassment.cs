using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHarassment : MonoBehaviour
{
    [SerializeField]
    private float _speedMove = 1f;
 
    [SerializeField]
    private Transform _player;
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
 
    private void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _speedMove * Time.deltaTime);
       
        transform.LookAt(_player, Vector3.up);



    }
}

