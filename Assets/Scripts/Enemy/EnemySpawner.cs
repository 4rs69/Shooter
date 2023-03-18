using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private PlayerMovementController _player;
    [SerializeField]
    private EnemyMovementController _enemyPrefab;
    [SerializeField] 
    private Transform[] _enemiesSpawnPoints;
    [SerializeField]
    private int _enemiesPerSpawnPoint = 5;
    [SerializeField]
    private int _randomRadius = 3;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach (var enemySpawnPoint in _enemiesSpawnPoints)
        {
            for (var i = 0; i < _enemiesPerSpawnPoint; i++)
            {
                var enemy = Instantiate(_enemyPrefab);
                var randomPosition = Random.insideUnitCircle * _randomRadius;
                
                var enemyPosition = enemySpawnPoint.position;
                enemyPosition.x += randomPosition.x;
                enemyPosition.z += randomPosition.y;
                enemy.transform.position = enemyPosition;
                
                enemy.Initialize(_player);
            }
        }
    }
}
