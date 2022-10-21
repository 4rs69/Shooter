using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructionBulletOnGround : MonoBehaviour
{
  
  private void OnCollisionEnter(Collision collision)
  {
    
    if (collision.gameObject.name is "Earth")
    {
      Destroy(gameObject);
    }
  }
}


