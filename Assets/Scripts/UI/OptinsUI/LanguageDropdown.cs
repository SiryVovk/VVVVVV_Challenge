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
            "Óêðà¿íñüêà"
        });

        dropdown.onValueChanged.AddListener(OnDropdownChanged);

        dropdown.value = (int)LocalizationManager.Instanñe.CurrentLanguage;
    }

    private void OnDropdownChanged(int index)
    {
        LocalizationManager.Instanñe.LoadLanguages((Languages)index);
    }
}
