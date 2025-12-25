using System;
using Unity.AppUI.UI;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rebinding : MonoBehaviour
{
    public Action OnReturnToDefaultBinding;
    public Action OnRebinding;

    private Player inputActions;

    private const string INPUT_BINDING = "InputBindings";

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private void Awake()
    {
        inputActions = InputManager.Instance.Controls;

        if (PlayerPrefs.HasKey(INPUT_BINDING))
        {
            inputActions.LoadBindingOverridesFromJson(
                PlayerPrefs.GetString(INPUT_BINDING)
            );
        }
    }

    public void StartRebinding(string actionName, int bindingIndex, Action<string> OnComplete)
    {
        InputAction action = inputActions.asset.FindAction(actionName);

        if (action == null)
        {
            Debug.LogError($"Action {actionName} not found");
            return;
        }

        action.Disable();

        rebindingOperation = action.PerformInteractiveRebinding(bindingIndex)
        .WithControlsExcluding("Mouse")
        .OnComplete(op =>
        {
           action.Enable();
           op.Dispose(); 

           SaveBinding();

            string keyName = InputControlPath.ToHumanReadableString(
                action.bindings[bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice
            );

            OnComplete?.Invoke(keyName);

        });

        rebindingOperation.Start();
        OnRebinding?.Invoke();
    }

    public void ResetBindings()
    {
        inputActions.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(INPUT_BINDING);
        OnReturnToDefaultBinding?.Invoke();
    }

    private void SaveBinding()
    {
        string json = inputActions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(INPUT_BINDING, json);
    }

    public string SetButton(string actionName, int bindingIndex)
    {
        if (inputActions == null)
        {
            Debug.LogError("Rebinding: inputActions is NULL");
            return "N/A";
        }

        InputAction action = inputActions.asset.FindAction(actionName);

        if (action == null)
        {
            Debug.LogError($"Rebinding: action '{actionName}' not found");
            return "N/A";
        }

        if (bindingIndex < 0 || bindingIndex >= action.bindings.Count)
        {
            Debug.LogError($"Rebinding: invalid bindingIndex {bindingIndex} for action {actionName}");
            return "N/A";
        }

        return InputControlPath.ToHumanReadableString(
            action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice
        );
    }

}
