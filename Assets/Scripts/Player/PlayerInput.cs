using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player;

public class PlayerInput : MonoBehaviour, IMovementActions
{
    public Action<float> OnMove;
    public Action OnGravity;

    private Player playerControls;

    private void Awake()
    {
        playerControls = new Player();
        playerControls.Movement.SetCallbacks(this);
        playerControls.Movement.Enable();
    }

    public void OnLeftRight(InputAction.CallbackContext context)
    {
        float direction = context.ReadValue<float>();
        OnMove?.Invoke(direction);
    }

    public void OnGravityChange(InputAction.CallbackContext context)
    {
        OnGravity?.Invoke();
    }
}
