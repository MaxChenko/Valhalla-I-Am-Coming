using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public enum LegState
{
    Idle,
    Running,
    Crouching,
    Falling,
    Jumping
}

[System.Serializable]
public enum ArmState
{
    Idle,
    Heavy,
    Projectile,
    Running,
    Holstering,
    Blocking
}

[System.Serializable]
public struct State
{
    public ArmState armState;
    public LegState legState;
    public bool movingBackwards;
    public Vector2 moveDirection;
    public Vector2 lookDirection;
    public bool holstered;

    public State(ArmState armState, LegState legState, Vector2 moveDirection, Vector2 lookDirection, bool holstered)
    {
        this.armState = armState;
        this.legState = legState;
        this.movingBackwards = !((moveDirection.x > 0 && lookDirection.x > 0) || (moveDirection.x < 0 && lookDirection.x < 0));
        this.moveDirection = moveDirection;
        this.lookDirection = lookDirection;
        this.holstered = holstered;
    }
}

[System.Serializable]
public class CharacterState
{
    //Flags for what the user is trying to achieve
    private bool _moveLeft;
    private bool _moveRight;
    private bool _jump;
    private bool _crouch;
    private bool _melee;
    private bool _projectile;
    private bool _holster;
    private bool _block;
    private Vector2 _lookDirection;

    //Flags for what is happening to the character
    private bool _grounded;
    private bool _canJump = true;
    private bool _performingAction;
    private Vector2 _moveDirection;
    
    //Current States
    private ArmState _armState;
    private LegState _legState;
    private bool _holstered;
    

    public State CalculateState(CharacterController controller)
    {
        var armState = _armState;
        var legState = _legState;
        var moveDirection = Vector2.zero;
        var lookDirection = _lookDirection;

        if (!_performingAction)
        {
            if (_projectile)
            {
                armState = ArmState.Projectile;
                controller.StartCoroutine(ProjectileRoutine());
            }
            else if (_melee)
            {
                armState = ArmState.Heavy;
                controller.StartCoroutine(MeleeRoutine());
            }
            else if (_holster)
            {
                armState = ArmState.Holstering;
                controller.StartCoroutine(HolsteringRoutine(controller));
            }
            else if (_block)
            {
                armState = ArmState.Blocking;
            }
            else if (_moveLeft || _moveRight)
            {
                armState = ArmState.Running;
            }
            else
            {
                armState = ArmState.Idle;
            }
        }

        if (_grounded)
        {
            if (_moveLeft || _moveRight)
            {
                moveDirection.x = _moveLeft ? Vector2.left.x : Vector2.right.x;
                legState = LegState.Running;
            }
            else if(_crouch)
            {
                moveDirection.x = 0;
                legState = LegState.Crouching;
            }
            else
            {
                moveDirection.x = 0;
                legState = LegState.Idle;
            }
            
            if (_jump && _canJump)
            {
                moveDirection.y = 1;
                legState = LegState.Jumping;
                controller.StartCoroutine(JumpRoutine());
            }

        }
        else
        {
            legState = LegState.Falling;
            moveDirection.y = 0;
            
            if (_moveLeft || _moveRight)
            {
                moveDirection.x = _moveLeft ? Vector2.left.x : Vector2.right.x;
            }
            else if(_crouch)
            {
                moveDirection.y = -1;
            }
            else
            {
                moveDirection.x = 0;
            }
        }

        _moveDirection = moveDirection;
        _armState = armState;
        _legState = legState;
        _lookDirection = lookDirection;
        return new State(armState, legState, moveDirection, lookDirection, _holstered);
    }

    private IEnumerator JumpRoutine()
    {
        _canJump = false;
        _legState = LegState.Jumping;
        
        yield return new WaitForSeconds(0.5f);

        _legState = LegState.Idle;
        _canJump = true;
    }
    
    private IEnumerator MeleeRoutine()
    {
        _performingAction = true;

        yield return new WaitForSeconds(1f);

        _performingAction = false;
        _armState = ArmState.Idle;
    }
    
    private IEnumerator ProjectileRoutine()
    {
        _performingAction = true;

        yield return new WaitForSeconds(1f);

        _performingAction = false;
        _armState = ArmState.Idle;
    }

    private IEnumerator HolsteringRoutine(CharacterController controller)
    {
        _performingAction = true;

        yield return new WaitForSeconds(0.25f);
        
        controller.URightAxe.SetActive(_holstered);
        controller.ULeftAxe.SetActive(_holstered);
        controller.HRightAxe.SetActive(!_holstered);
        controller.HLeftAxe.SetActive(!_holstered);
        
        yield return new WaitForSeconds(0.25f);
        
        _holstered = !_holstered;
        _performingAction = false;
        _armState = ArmState.Idle;
    }
    
    //Getters and Setters
    public bool MoveLeft
    {
        get => _moveLeft;
        set => _moveLeft = value;
    }
    public bool MoveRight
    {
        get => _moveRight;
        set => _moveRight = value;
    }
    public bool Jump
    {
        get => _jump;
        set => _jump = value;
    }
    public bool Crouch
    {
        get => _crouch;
        set => _crouch = value;
    }
    public bool Melee
    {
        get => _melee;
        set => _melee = value;
    }
    public bool Projectile
    {
        get => _projectile;
        set => _projectile = value;
    }
    public Vector2 LookDirection
    {
        get => _lookDirection;
        set => _lookDirection = value;
    }
    public bool Grounded
    {
        get => _grounded;
        set => _grounded = value;
    }
    public bool Holster
    {
        get => _holster;
        set => _holster = value;
    }

    public bool Block
    {
        get => _block;
        set => _block = value;
    }
    public bool PerformingAction
    {
        get => _performingAction;
        set => _performingAction = value;
    }
    public Vector2 MoveDirection
    {
        get => _moveDirection;
        set => _moveDirection = value;
    }
}