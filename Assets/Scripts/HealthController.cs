using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public float MaxHealth => _maxHealth;
    
    [SerializeField]
    private UnityEvent _diedEvent;
    [SerializeField]
    private UnityEvent<float> _decreaseHealthEvent;
    
    [SerializeField] 
    private float _maxHealth = 5;

    private float _currentHealth;
    
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _decreaseHealthEvent.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            _diedEvent.Invoke();
        }
    }
}
