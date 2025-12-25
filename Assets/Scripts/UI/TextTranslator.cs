using TMPro;
using UnityEngine;

public class TextTranslator : MonoBehaviour
{
    [SerializeField] private string key;
    
    private TMP_Text tMP_Text;

    private void Awake()
    {
        tMP_Text = GetComponent<TMP_Text>();
    }

    private void OnDestroy()
    {
        if(LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.OnLanguageChange -= OnLanguageChange;
        }
    }

    private void Start()
    {
        OnLanguageChange();
        LocalizationManager.Instance.OnLanguageChange += OnLanguageChange;
    }

    private void OnLanguageChange()
    {
        tMP_Text.text = LocalizationManager.Instance.Get(key);
    }
}
