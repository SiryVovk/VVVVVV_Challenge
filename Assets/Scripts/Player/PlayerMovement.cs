using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Action<float, float> OnMove;

    [SerializeField] private SoundData jumpSound;
    [SerializeField] private SoundData walkSound;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private SoundEmiter walkSoundEmitter;

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
        
        OnMove?.Invoke(velocity.x, velocity.y);
    }

    private void WalkSoundControl()
    {
        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f && IsOnGrond() && walkSoundEmitter == null)
        {
            SoundBuilder soundBuilder = SoundPool.Instance.CreateSoundBuilder()
                .WithSoundData(walkSound)
                .AtPosition(transform.position);
            walkSoundEmitter = soundBuilder.Play();
        }
        else
        {
            if (walkSoundEmitter != null)
            {
                walkSoundEmitter.StopSound();
                walkSoundEmitter = null;
            }
        }
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

        SoundBuilder soundBuilder = SoundPool.Instance.CreateSoundBuilder()
            .WithSoundData(jumpSound)
            .AtPosition(transform.position);
        soundBuilder.Play();
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
}
