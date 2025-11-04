using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private float moveDirection;
    private bool isOnGround = true;

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
    }

    private void Update()
    {
        Move();     
    }

    private void Move()
    {
        Vector3 movement = new Vector3(moveDirection, 0, 0) * Time.deltaTime * speed;
        transform.Translate(movement);
    }

    private void HandleMove(float direction)
    {
        moveDirection = direction;
    }

    private void HandleGravityChange()
    {
        if (!isOnGround)
        {
            return;
        }

        rb.gravityScale = -rb.gravityScale;
        isOnGround = false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
