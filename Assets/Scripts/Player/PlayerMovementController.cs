using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] 
    private UnityEvent<Vector2, float> _playerMoved;
    [SerializeField] 
    private UnityEvent _playerJumped;
    
    [SerializeField]
    private float _gravity = -9.81f;
    [SerializeField] 
    private float _jumpHeight = 1;
    [SerializeField] 
    private float _stepDown = 0.03f;
    [SerializeField]
    private float _speedChangeRate = 10;
    [SerializeField]
    private float _moveSpeed = 2;
    [SerializeField]
    private float _sprintSpeed = 6;
    [SerializeField]
    private float _speedOffset = 0.1f;
    
    private float _targetSpeed;
    private float _currentSpeed;
    private float _verticalVelocity;
    
    private Vector2 _movingDirection;
    private Vector2 _jumpingDirection;
    private Vector3 _rootMotion;
    private bool _isJumping;

    private Animator _animator;
    private CharacterController _characterController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        if (_isJumping)
        {
            var velocity = _animator.velocity;
            velocity.y = _verticalVelocity;
            
            var movementDelta = velocity * Time.deltaTime;
            movementDelta += (transform.forward * _jumpingDirection.y + 
                              transform.right * _jumpingDirection.x) * _moveSpeed/100;
            _characterController.Move(movementDelta);
            
            _isJumping = !_characterController.isGrounded;
            _rootMotion = Vector3.zero;
        }
        else
        {
            _characterController.Move(_rootMotion + Vector3.down * _stepDown);
            _rootMotion = Vector3.zero;
        }

        ApplyGravity();
    }

    public void StartMove()
    {
        _targetSpeed = _moveSpeed;
    }
    
    public void FinishMove()
    { 
        _targetSpeed = 0;
    }

    public void SetMovingDirection(Vector2 direction)
    {
        _movingDirection = direction;
    }

    public void StartRun()
    {
        _targetSpeed = _sprintSpeed;
    }
    
    public void FinishRun()
    {
        _targetSpeed = _moveSpeed;
    }

    public void Jump()
    {
        if (!_isJumping)
        {
            _isJumping = true;
            _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            _playerJumped.Invoke();
        }
    }
    
    public void SetJumpingDirection(Vector2 direction)
    {
        _jumpingDirection = direction;
    }

    /// <summary>
    /// Метод вызывается автоматически при движении при помощи аниматора (rootMotion)
    /// </summary>
    private void OnAnimatorMove()
    {
        _rootMotion += _animator.deltaPosition;
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity = 0;
        }
        else
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }
    }
    
    private void Move()
    {
        if (_currentSpeed < _targetSpeed - _speedOffset || _currentSpeed > _targetSpeed + _speedOffset)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed,
                Time.deltaTime * _speedChangeRate);
        }
        else
        {
            _currentSpeed = _targetSpeed;
        }
        
        _playerMoved.Invoke(_movingDirection, _currentSpeed);
    }
}