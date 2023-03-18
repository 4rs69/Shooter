using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] 
    private Weapon _prefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out UnitFacade unitFacade))
        {
            var newWeapon = Instantiate(_prefab);
            unitFacade.Equip(newWeapon);
            Destroy(gameObject); 
        }
    }
}