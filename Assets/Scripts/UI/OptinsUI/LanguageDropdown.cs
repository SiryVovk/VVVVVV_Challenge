using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Icons;

public class LanguageDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown.ClearOptions();

        dropdown.AddOptions(new List<string>
        {   "English",
            "Українська"
        });

        dropdown.onValueChanged.AddListener(OnDropdownChanged);

        dropdown.value = (int)LocalizationManager.Instance.CurrentLanguage;
    }

    private void OnDropdownChanged(int index)
    {
        LocalizationManager.Instance.LoadLanguages((Languages)index);
    }
}
