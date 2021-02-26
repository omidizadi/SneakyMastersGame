using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour, IPlayerLocomotion
{
    [SerializeField] private Animator playerAnimator;
    private static readonly int Sprint = Animator.StringToHash("sprint");
    private static readonly int Idle = Animator.StringToHash("idle");
    private static readonly int Shoot = Animator.StringToHash("shoot");
    private static readonly int Dance = Animator.StringToHash("dance");

    public void DoSprint()
    {
        playerAnimator.SetBool(Sprint, true);
        playerAnimator.SetBool(Idle, false);
    }

    public void DoIdle()
    {
        playerAnimator.SetBool(Sprint, false);
        playerAnimator.SetBool(Idle, true);
    }

    public void DoShoot()
    {
        playerAnimator.SetTrigger(Shoot);
    }
    public void DoDance()
    {
        playerAnimator.SetTrigger(Dance);
    }
    
    
}
