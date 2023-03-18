using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private PlayerMovementController _player;

    public void Initialize(PlayerMovementController player)
    {
        _player = player;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.enabled = true;
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }
}
