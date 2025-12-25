using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public Action OnLanguageChange;
    public static LocalizationManager Instance {get; private set;}
    public Languages CurrentLanguage => currentLanguage;

    private Dictionary<string, string> localizedText;
    private Languages currentLanguage;
    private static string LANGUAGE_KEY = "Language";

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
        DontDestroyOnLoad(this);

        currentLanguage = (Languages)PlayerPrefs.GetInt(LANGUAGE_KEY, 0);
        LoadLanguages(currentLanguage);

    }

    public void LoadLanguages(Languages languages)
    {
        currentLanguage = languages;
        PlayerPrefs.SetInt(LANGUAGE_KEY, (int)currentLanguage);

        string fileName = languages == Languages.English ? "en" : "uk";
        TextAsset jsonFile = Resources.Load<TextAsset>($"Localization/{fileName}");

        localizedText = JsonUtility
        .FromJson<LocalizationWrapper>(jsonFile.text)
        .ToDictionary();

        OnLanguageChange?.Invoke();
    }

    public string Get(string key)
    {
        if (localizedText.TryGetValue(key, out var value))
        {
            return value;
        }

        return $"#{key}";
    }
}
