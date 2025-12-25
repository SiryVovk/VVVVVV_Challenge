using System;
using NUnit.Framework;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, ILoadable
{
    public Action<float, float> OnMove;
    public Action OnGravityChange;

    [SerializeField] private SoundData jumpSound;
    [SerializeField] private SoundData walkSound;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5f;

    private PlayerInputController playerInput;
    private Rigidbody2D rb;

    private SoundEmiter walkSoundEmitter;

    private float moveDirection;

    private bool gravitySwitchLocked = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInputController>();
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

        if(IsOnGrond())
        {
            gravitySwitchLocked = false;
        }
    }

    private void Move()
    {
        Vector2 velocity = rb.linearVelocity;

        velocity.x = moveDirection * speed;

        rb.linearVelocity = velocity;
        
        WalkSoundControl();
        OnMove?.Invoke(velocity.x, velocity.y);
    }

    private void WalkSoundControl()
    {
        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f && !gravitySwitchLocked && walkSoundEmitter == null)
        {
            SoundBuilder soundBuilder = SoundPool.Instance.CreateSoundBuilder()
                .WithSoundData(walkSound)
                .AtPosition(transform.position);
            walkSoundEmitter = soundBuilder.Play();
        }
        else if(Mathf.Abs(rb.linearVelocity.x) <= 0.1f || IsOnGrond() == false)
        {
            if(walkSoundEmitter !=null)
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

        if (gravitySwitchLocked)
        {
            return;
        }

        if(!IsOnGrond())
        {
            return;
        }

        gravitySwitchLocked = true;

        SoundBuilder soundBuilder = SoundPool.Instance.CreateSoundBuilder()
            .WithSoundData(jumpSound)
            .AtPosition(transform.position);
        soundBuilder.Play();
        rb.gravityScale = -rb.gravityScale;
        OnGravityChange?.Invoke();
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
        transform.position = new Vector3(SaveManager.CurrentSave.xPlayerPosition, SaveManager.CurrentSave.yPlayerPosition, SaveManager.CurrentSave.zPlayerPosition);
    }
}
