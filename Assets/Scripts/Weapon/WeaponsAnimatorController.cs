using System.Collections;
using UnityEngine;

public class WeaponsAnimatorController : MonoBehaviour
{
    private static readonly int IsWeaponActivated = Animator.StringToHash("isWeaponActivated");
    private Animator _animator;
    private Coroutine _switchWeaponCoroutine;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = true;
    }

    public void PlayChangeWeaponStateAnimation()
    {
        var isWeaponActivated = _animator.GetBool(IsWeaponActivated); 
        _animator.SetBool(IsWeaponActivated, !isWeaponActivated);
    }

    public void PlaySwitchWeaponAnimation(WeaponType weaponType)
    {
        if (_switchWeaponCoroutine != null)
        {
            StopCoroutine(_switchWeaponCoroutine);
        }
        
        _switchWeaponCoroutine = StartCoroutine(SwitchWeapon(weaponType));
    }

    private IEnumerator SwitchWeapon(WeaponType weaponType)
    {
        var isDeactivated = !_animator.GetBool(IsWeaponActivated);
        
        if (!isDeactivated)
        {
            yield return StartCoroutine(DeactivateWeapon());
        }
        
        yield return StartCoroutine(ActivateWeapon(weaponType));
    }
    
    private IEnumerator DeactivateWeapon()
    {
        _animator.SetBool(IsWeaponActivated, false);
        
        var currentState = _animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        
        while (currentState == _animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
        {
            yield return null;
        }
        
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
    }
    
    private IEnumerator ActivateWeapon(WeaponType weaponType)
    {
        _animator.SetBool(IsWeaponActivated, true);
        _animator.Play("equip_" + weaponType);
        
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
    }
}