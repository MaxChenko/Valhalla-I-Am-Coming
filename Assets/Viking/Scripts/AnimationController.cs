using System;
using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public CharacterController characterController;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        characterController.OnCharacterAction += Animation;
    }

    private void OnDisable()
    {
        characterController.OnCharacterAction -= Animation;
    }


    private void Animation(State state)
    {
        switch (state.legState)
        {
            case LegState.Running when state.movingBackwards:
                animator.Play("BackwardRun",0);
                break;
            case LegState.Running:
                animator.Play("ForwardRun",0);
                break;
            case LegState.Idle:
                animator.Play("Idle",0);
                break;
            case LegState.Crouching:
                animator.Play("Crouch",0);
                break;
            case LegState.Falling:
                break;
            case LegState.Jumping:
                break;
        }
        
        switch (state.armState)
        {
            case ArmState.Idle:
                animator.Play("Idle",1);
                break;
            case ArmState.Heavy when state.holstered:
                animator.Play("Punch",1);
                break;
            case ArmState.Heavy:
                animator.Play("Melee",1);
                break;
            case ArmState.Projectile when !state.holstered:
                animator.Play("Projectile",1);
                break;
            case ArmState.Running:
                animator.Play("Run",1);
                break;
            case ArmState.Holstering when state.holstered:
                animator.Play("Unholster",1);
                break;
            case ArmState.Holstering:
                animator.Play("Holster",1);
                break;
            case ArmState.Blocking:
                break;
        }
    }

}
