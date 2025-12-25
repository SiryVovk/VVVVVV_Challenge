using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player;

public class PlayerInputController : MonoBehaviour, IMovementActions
{
    public Action<float> OnMove;
    public Action OnGravity;

    private void OnEnable()
    {
        var control = InputManager.Instance.Controls;
        control.Movement.AddCallbacks(this);
        control.Movement.Enable();
    }

    private void OnDisable()
    {
        var control = InputManager.Instance.Controls;
        control.Movement.Disable();
        control.Movement.RemoveCallbacks(this);
    }

    public void OnLeftRight(InputAction.CallbackContext context)
    {
        float direction = context.ReadValue<float>();
        OnMove?.Invoke(direction);
    }

    public void OnGravityChange(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        OnGravity?.Invoke();
    }
}
