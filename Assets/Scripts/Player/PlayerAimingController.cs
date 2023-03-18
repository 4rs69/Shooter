using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAimingController : MonoBehaviour
{
    [SerializeField]
    private Rig _bodyLayer;
    
    [SerializeField]
    private MultiAimConstraint _aimLayer;
    
    public void GetIntoShootingPose()
    {
        _aimLayer.weight = 1;
        _bodyLayer.weight = 1;
    }
    
    public void GetOutShootingPose()
    {
        _aimLayer.weight = 0;
        _bodyLayer.weight = 0;
    }
}