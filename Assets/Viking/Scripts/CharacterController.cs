using System;
using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CharacterState state;
    public GameObject HLeftAxe;
    public GameObject HRightAxe;
    public GameObject ULeftAxe;
    public GameObject URightAxe;
    public Transform Body;
    public Transform Head;
    
    public delegate void CharacterAction(State state);
    public event CharacterAction OnCharacterAction;

    private void Awake()
    {
        state = new CharacterState();
    }

    private void OnEnable()
    {
        InputController.OnWPressed += Jump;
        InputController.OnAPressed += MoveLeft;
        InputController.OnSPressed += Crouch;
        InputController.OnDPressed += MoveRight;
        InputController.OnMouseLeftPressed += HeavyAttack;
        InputController.OnMouseRightPressed += LightAttack;
        InputController.OnSpacePressed += Block;
        InputController.OnEPressed += ToggleHolster;
        
        InputController.OnWReleased += StopJump;
        InputController.OnAReleased += StopMoveLeft;
        InputController.OnSReleased += StopCrouch;
        InputController.OnDReleased += StopMoveRight;
        InputController.OnMouseLeftReleased += StopHeavyAttack;
        InputController.OnMouseRightReleased += StopLightAttack;
        InputController.OnSpaceReleased += StopBlock;
        InputController.OnEReleased += StopToggleHolster;

        InputController.OnMousePositionChanged += SetLookDirection;
    }

    private void OnDisable()
    {
        InputController.OnWPressed -= Jump;
        InputController.OnAPressed -= MoveLeft;
        InputController.OnSPressed -= Crouch;
        InputController.OnDPressed -= MoveRight;
        InputController.OnMouseLeftPressed -= HeavyAttack;
        InputController.OnMouseRightPressed -= LightAttack;
        InputController.OnSpacePressed -= Block;
        InputController.OnEPressed -= ToggleHolster;
        
        InputController.OnWReleased -= StopJump;
        InputController.OnAReleased -= StopMoveLeft;
        InputController.OnSReleased -= StopCrouch;
        InputController.OnDReleased -= StopMoveRight;
        InputController.OnMouseLeftReleased -= StopHeavyAttack;
        InputController.OnMouseRightReleased -= StopLightAttack;
        InputController.OnSpaceReleased -= StopBlock;
        InputController.OnEReleased -= StopToggleHolster;
        
        InputController.OnMousePositionChanged -= SetLookDirection;
    }

    private void Update()
    {
        var s = state.CalculateState(this);
        OnCharacterAction?.Invoke(s);
    }

    private void StopToggleHolster()
    {
        state.Holster = false;
    }

    private void StopBlock()
    {
        state.Block = false;
    }

    private void StopLightAttack()
    {
        state.Projectile = false;
    }

    private void StopHeavyAttack()
    {
        state.Melee = false;
    }

    private void StopMoveRight()
    {
        state.MoveRight = false;
    }

    private void StopCrouch()
    {
        state.Crouch = false;
    }

    private void StopMoveLeft()
    {
        state.MoveLeft = false;
    }

    private void StopJump()
    {
        state.Jump = false;
    }

    private void SetLookDirection(Vector2 position)
    {
        var difference = new Vector3(position.x, position.y) - Head.position;
        var dir = difference.normalized;
        state.LookDirection = dir;
        transform.rotation = Quaternion.Euler(0, dir.x > 0 ? 0 : 180, 0);

        var angle = Mathf.Atan2(dir.y,dir.x < 0 ? -dir.x : dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -30f, 30f);
        Body.localEulerAngles = new Vector3(0, 0, angle);
    }

    private void ToggleHolster()
    {
        state.Holster = true;
    }

    private void Block()
    {
        state.Block = true;
    }

    private void LightAttack()
    {
        state.Projectile = true;
    }

    private void HeavyAttack()
    {
        state.Melee = true;
    }

    private void MoveRight()
    {
        state.MoveRight = true;
    }

    private void Crouch()
    {
        state.Crouch = true;
    }

    private void MoveLeft()
    {
        state.MoveLeft = true;
    }

    private void Jump()
    {
        state.Jump = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            state.Grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            state.Grounded = false;
        }
    }
}
