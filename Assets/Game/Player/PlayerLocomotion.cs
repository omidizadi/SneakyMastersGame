using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    private static readonly int Sprint = Animator.StringToHash("sprint");
    private static readonly int Idle = Animator.StringToHash("idle");

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
    
    
}
