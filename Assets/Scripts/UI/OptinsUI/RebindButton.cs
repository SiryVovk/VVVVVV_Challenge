using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RebindButton : MonoBehaviour
{
    [SerializeField] private Rebinding rebinding;

    [SerializeField] private string actionName;
    [SerializeField] private int bindingIndex;

    [SerializeField] private TMP_Text bindingText;
    [SerializeField] private Button button;

    private void OnEnable()
    {
        rebinding.OnReturnToDefaultBinding += UpdateText;
    }

    private void OnDisable()
    {
        rebinding.OnReturnToDefaultBinding -= UpdateText;
    }

    private void Start()
    {
        UpdateText();
    }

    public void OnPressed()
    {
        bindingText.text = "Press any key";
        button.interactable = false;

        rebinding.StartRebinding(
            actionName,
            bindingIndex,
            OnRebindComplete
        );
    }

    private void OnRebindComplete(string newKey)
    {
        bindingText.text = newKey;
        button.interactable = true;
    }

    private void UpdateText()
    {
        bindingText.text = rebinding.SetButton(actionName, bindingIndex);
    }
}
