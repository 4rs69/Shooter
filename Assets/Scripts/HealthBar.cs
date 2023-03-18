using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private HealthController _healthController;

    private void Start()
    {
        SetHealth(_healthController.MaxHealth);
    }

    [UsedImplicitly]
    public void SetHealth(float currentHealth)
    {
        _healthBar.value = currentHealth / _healthController.MaxHealth;

        if (_healthBar.value <= 0)
        {
            _healthBar.fillRect.gameObject.SetActive(false);
        }
    }
}
