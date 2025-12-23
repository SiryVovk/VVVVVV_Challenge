using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player;

public class PlayerInputController : MonoBehaviour, IMovementActions
{
    public Action<float> OnMove;
    public Action OnGravity;

    private Player playerControls;

    private const string INPUT_BINDING = "InputBindings";

    private void Awake()
    {
        playerControls = new Player();
        playerControls.Movement.SetCallbacks(this);
        playerControls.Movement.Enable();

        if (PlayerPrefs.HasKey(INPUT_BINDING))
        {
            playerControls.LoadBindingOverridesFromJson(
                PlayerPrefs.GetString(INPUT_BINDING)
            );
        }
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
