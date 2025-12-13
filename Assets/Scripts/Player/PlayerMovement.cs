using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float speed = 5f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private float moveDirection;
    private bool isGravityInverted = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.OnMove += HandleMove;
        playerInput.OnGravity += HandleGravityChange;
    }

    private void OnDestroy()
    {
        playerInput.OnMove -= HandleMove;
        playerInput.OnGravity -= HandleGravityChange;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 velocity = rb.linearVelocity;

        velocity.x = moveDirection * speed;

        rb.linearVelocity = velocity;
    }

    private void HandleMove(float direction)
    {
        moveDirection = direction;
    }

    private void HandleGravityChange()
    {
        if(!IsOnGrond())
        {
            return;
        }

        rb.gravityScale = -rb.gravityScale;
    }
    
    private bool IsOnGrond()
    {
        return rb.IsTouchingLayers(groundLayer);
    }
}
