using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController characterController;

    private State state;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        characterController.OnCharacterAction += CopyState;
    }

    private void OnDisable()
    {
        characterController.OnCharacterAction -= CopyState;
    }

    private void FixedUpdate()
    {
        if (state.legState is LegState.Falling) return;
        
        if (state.moveDirection.x == 0)
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
        else if (state.moveDirection.x < 0)
        {
            rb.velocity = new Vector2(-5f,rb.velocity.y);
        }
        else if (state.moveDirection.x > 0)
        {
            rb.velocity = new Vector2(5f,rb.velocity.y);
        }
    }
    

    private void CopyState(State state)
    {
        if (state.moveDirection.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 20f);
        }else if (state.moveDirection.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -20f);
        }
        this.state = state;
    }
}
