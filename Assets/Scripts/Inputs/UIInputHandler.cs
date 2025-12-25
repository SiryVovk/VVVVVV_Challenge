using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player;

public class UIInputHandler : MonoBehaviour, IMenuActions
{
    public Action Escape;

    private void OnEnable()
    {
        var controls = InputManager.Instance.Controls;
        controls.Menu.AddCallbacks(this);
        controls.Menu.Enable();
    }

    private void OnDisable()
    {
        var controls = InputManager.Instance.Controls;
        controls.Menu.Disable();
        controls.Menu.RemoveCallbacks(this);
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        Escape?.Invoke();
    }
}
