using UnityEngine;

public class PlayerMovement : MonoBehaviour, ILoadable
{
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float speed = 5f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private float moveDirection;


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

    public void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = Mathf.Abs(rb.gravityScale);
    }

    public void LoadData()
    {
        transform.position = new Vector3(SaveManager.CurrentSave.xPlayerPosition, SaveManager.CurrentSave.yPlayerPosition, SaveManager.CurrentSave.yPlayerPosition);
    }
}
