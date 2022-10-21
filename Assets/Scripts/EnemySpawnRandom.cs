using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnRandom : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    
   private void Start()
    {
        
        var random = Random.Range(5, 10);
        for (int i = 0; i < random; i++)
        {
            var newEnemy = Instantiate(_enemy);
            newEnemy.transform.position = new Vector3(Random.Range(-30, 30), 0.1f, Random.Range(50, -40));
        }

        

    }
   
}
