using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerRotationController))]
[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(WeaponsController))]
[RequireComponent(typeof(PlayerAimingController))]
[RequireComponent(typeof(PlayerAnimatorController))]
public class UnitFacade : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onAimModeEnabled;
    [SerializeField]
    private UnityEvent _onAimModeDisabled;
    
    private InputActions _inputActions; 
    private PlayerRotationController _rotationController;
    private PlayerMovementController _movementController;
    private WeaponsController _weaponsController;
    private PlayerAimingController _aimingController;
    private PlayerAnimatorController _animatorController;

    private bool _hasWeapon;
    private bool _isAiming;
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        
        _inputActions = new InputActions();
        _rotationController = GetComponent<PlayerRotationController>();
        _movementController = GetComponent<PlayerMovementController>();
        _weaponsController = GetComponent<WeaponsController>();
        _aimingController = GetComponent<PlayerAimingController>();
        _animatorController = GetComponent<PlayerAnimatorController>();
        
        SubscribeInputActionsEvents();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }
    
    public void Equip(Weapon newWeapon)
    {
        _weaponsController.Equip(newWeapon);
        _hasWeapon = true;
        
        _isAiming = true;
        TakeAim();
    }

    private void StartMove(InputAction.CallbackContext callbackContext)
    {
        var inputValue = callbackContext.ReadValue<Vector2>();
        _rotationController.SetRotationDirection(inputValue);
        _movementController.SetMovingDirection(inputValue);
        
        if (_inputActions.Player.Sprint.IsInProgress())
        {
            return;
        }
        
        _movementController.StartMove();
    }

    private void FinishMove(InputAction.CallbackContext callbackContext)
    {
        _movementController.FinishMove();
        _movementController.SetMovingDirection(Vector2.zero);
        _rotationController.SetRotationDirection(Vector2.zero);
    }

    private void StartRun(InputAction.CallbackContext callbackContext)
    {
        if (_inputActions.Player.Move.IsInProgress())
        {
            _movementController.StartRun();
        }
    }
    
    private void FinishRun(InputAction.CallbackContext callbackContext)
    {
        if (_inputActions.Player.Move.IsInProgress())
        {
            _movementController.FinishRun();
        }
    }

    private void ChangeWeaponState(InputAction.CallbackContext callbackContext)
    {
        if (!_hasWeapon)
        {
            return;
        }

        _weaponsController.ChangeWeaponState();
        _isAiming = !_isAiming;

        if (_isAiming)
        {
            TakeAim();
        }
        else
        {
            DenyAim();
        }
    }

    private void SwitchWeapon(InputAction.CallbackContext callbackContext)
    {
        if (!_hasWeapon)
        {
            return;
        }
        
        var canSwitch = _weaponsController.TryToSwitchWeapon();
        if (!canSwitch)
        {
            return;
        }
        
        _isAiming = true;
        TakeAim();
    }
    
    private void Fire(InputAction.CallbackContext callbackContext)
    {
        if (_isAiming)
        {
            _weaponsController.Shoot();
        }
    }
    
    private void Jump(InputAction.CallbackContext callbackContext)
    {    
        var movingInputValue = _inputActions.Player.Move.ReadValue<Vector2>();
        if (_isAiming)
        {
            _movementController.SetJumpingDirection(movingInputValue);
        }
        else
        {
            var isMoving = movingInputValue != Vector2.zero;
            _movementController.SetJumpingDirection(isMoving ? Vector2.up : Vector2.zero); 
        }

        _movementController.Jump();
    }

    private void TakeAim()
    {
        _rotationController.SetRotation(false);
        _aimingController.GetIntoShootingPose();
        _animatorController.TransitToWalkingWithWeaponBlendTree();
        
        _onAimModeEnabled.Invoke();
    }

    private void DenyAim()
    {
        _rotationController.SetRotation(true);
        _aimingController.GetOutShootingPose();
        _animatorController.TransitToWalkingWithoutWeaponBlendTree();
        
        _onAimModeDisabled.Invoke();
    }
	
    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }
    
    private void SubscribeInputActionsEvents()
    {
        _inputActions.Player.Move.performed += StartMove;
        _inputActions.Player.Move.canceled += FinishMove;
        _inputActions.Player.Sprint.performed += StartRun;
        _inputActions.Player.Sprint.canceled += FinishRun;  
        _inputActions.Player.ChangeWeaponState.performed += ChangeWeaponState;
        _inputActions.Player.SwitchWeapon.performed += SwitchWeapon;
        _inputActions.Player.Fire.performed += Fire;
        _inputActions.Player.Jump.performed += Jump;
    }

    private void UnsubscribeInputActionsEvents()
    {
        _inputActions.Player.Move.performed -= StartMove;
        _inputActions.Player.Move.canceled -= FinishMove;
        _inputActions.Player.Sprint.performed -= StartRun;
        _inputActions.Player.Sprint.canceled -= FinishRun;
        _inputActions.Player.ChangeWeaponState.performed -= ChangeWeaponState;
        _inputActions.Player.SwitchWeapon.performed -= SwitchWeapon;
        _inputActions.Player.Fire.performed -= Fire;
        _inputActions.Player.Jump.performed -= Jump;
    }

    private void OnDestroy()
    {
        UnsubscribeInputActionsEvents();
    }
}