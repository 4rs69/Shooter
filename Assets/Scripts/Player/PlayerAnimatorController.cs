using JetBrains.Annotations;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int Death = Animator.StringToHash("death");
    private static readonly int HasWeapon = Animator.StringToHash("hasWeapon");
    private static readonly int Horizontal = Animator.StringToHash("horizontal");
    private static readonly int Vertical = Animator.StringToHash("vertical");
    private static readonly int Jumping = Animator.StringToHash("jumping");
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    [UsedImplicitly]
    public void PlayMovementAnimation(Vector2 direction, float speed)
    {
        _animator.SetFloat(Horizontal, direction.x * speed);
        _animator.SetFloat(Vertical, direction.y * speed);
        _animator.SetFloat(Speed, speed); 
    }

    [UsedImplicitly]
    public void PlayDeathAnimation()
    {
        _animator.SetTrigger(Death);
    }
    
    public void PlayJumpAnimation()
    {
        _animator.SetTrigger(Jumping);
    }
    
    public void TransitToWalkingWithWeaponBlendTree()
    {
        _animator.SetBool(HasWeapon, true);
    }
    
    public void TransitToWalkingWithoutWeaponBlendTree()
    {
        _animator.SetBool(HasWeapon, false);
    }
}