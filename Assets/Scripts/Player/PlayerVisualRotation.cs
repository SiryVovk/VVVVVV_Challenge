using UnityEngine;

public class PlayerVisualRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private LayerMask groundLayer;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.OnGravity += HandleGravityChange;
    }

    private void HandleGravityChange()
    {
        if(!IsOnGrond())
        {
            return;
        }
        
        playerSpriteRenderer.flipY = !playerSpriteRenderer.flipY;
    }

    private bool IsOnGrond()
    {
        return rb.IsTouchingLayers(groundLayer);
    }
}
