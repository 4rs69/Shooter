using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public WeaponType WeaponType => _weaponType;
    
    [SerializeField] 
    private UnityEvent _onShoot;
    [SerializeField]
    private WeaponType _weaponType;
    [SerializeField]
    private float _damageValue = 1;
    [SerializeField]
    private float _shootRange = 100;
    [SerializeField]
    private ParticleSystem _impactEffect;
    
    private Camera _camera;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    public void Shoot()
    {
        var ray = new Ray(_camera.transform.position, _camera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _shootRange))
        {
            if (hit.collider.TryGetComponent(out HealthController healthController))
            {
                healthController.TakeDamage(_damageValue);
            }
            
            Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
        
        _onShoot.Invoke();
    }
}
