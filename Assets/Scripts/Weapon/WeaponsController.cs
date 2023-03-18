using UnityEngine;
using UnityEngine.Events;

public class WeaponsController : MonoBehaviour 
{
    [SerializeField] 
    private UnityEvent _onShoot;

    [SerializeField] 
    private Transform[] _weaponSlots;
    private Weapon[] _weapons;
    private int _selectedWeaponIndex;
    private WeaponsAnimatorController _weaponsAnimator;

    private void Awake()
    {
        _weapons = new Weapon[_weaponSlots.Length];

        _weaponsAnimator = GetComponentInChildren<WeaponsAnimatorController>();
        var weapon = GetComponentInChildren<Weapon>();
        if (weapon)
        {
            Equip(weapon);
        }
    }

    public void Equip(Weapon newWeapon)
    {
        var weaponSlotIndex = (int) newWeapon.WeaponType;
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }

        weapon = newWeapon;
        weapon.transform.SetParent(_weaponSlots[weaponSlotIndex], false);
        
        _weapons[weaponSlotIndex] = weapon;
        _selectedWeaponIndex = weaponSlotIndex;
        SwitchWeapon(weapon.WeaponType);
    }

    public bool TryToSwitchWeapon()
    {
        var newWeaponIndex = (_selectedWeaponIndex + 1) % _weapons.Length;
        var newWeapon = GetWeapon(newWeaponIndex);

        if (!newWeapon)
        {
            return false;
        }
        
        SwitchWeapon(newWeapon.WeaponType);
        _selectedWeaponIndex = newWeaponIndex;
        return true;
    }

    public void ChangeWeaponState()
    {
        _weaponsAnimator.PlayChangeWeaponStateAnimation();
    }
    
    private void SwitchWeapon(WeaponType weaponType)
    {
        _weaponsAnimator.PlaySwitchWeaponAnimation(weaponType);
    }

    public void Shoot()
    {
        var weapon = GetWeapon(_selectedWeaponIndex);
        weapon.Shoot();
        _onShoot.Invoke();
    }

    private Weapon GetWeapon(int index)
    {
        if (index < 0 || index >= _weapons.Length)
        {
            return null;
        }

        return _weapons[index];
    }
}