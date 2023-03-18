using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _enemyAttackedEvent;
    [SerializeField]
    private float _attackDelay = 2f;
    [SerializeField] 
    private float _damageValue = 1;
    [SerializeField] 
    private Transform _damageZone;
    [SerializeField] 
    private float _damageZoneRadius = 1;
    [SerializeField] 
    private LayerMask _layers;

    private WaitForSeconds _waitingBetweenAttack;
    private Coroutine _attackCoroutine;
    
    private void Awake()
    {
        _waitingBetweenAttack = new WaitForSeconds(_attackDelay);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            return;
        }
        
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
            
        _attackCoroutine = StartCoroutine(StartAttack());
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            StopCoroutine(_attackCoroutine);
        }
    }
    
    [UsedImplicitly]
    public void DealDamage()
    {
        var hitColliders = Physics.OverlapSphere(_damageZone.position, _damageZoneRadius, _layers);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.TryGetComponent(out HealthController player))
            {
                player.TakeDamage(_damageValue);
            }
        }
    }

    private IEnumerator StartAttack()
    {
        while (true)
        {
            _enemyAttackedEvent.Invoke();
            yield return _waitingBetweenAttack;
        }
    }
}