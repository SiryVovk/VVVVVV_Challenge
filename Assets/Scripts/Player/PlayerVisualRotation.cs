using UnityEngine;

public class PlayerVisualRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private LayerMask groundLayer;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.OnGravityChange += HandleGravityChange;
    }

    private void HandleGravityChange()
    {
        playerSpriteRenderer.flipY = !playerSpriteRenderer.flipY;
    }

    public void Respawn()
    {
        playerSpriteRenderer.flipY = false;
    }
}
