using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuMainButtons : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject optionsMenuUI;
    [SerializeField] private GameObject creditsMenuUI;
    [SerializeField] private Button continueButton;

    private const int FIRST_LEVEL_INDEX = 1;

    private void Start()
    {
        if(SaveManager.HasSave())
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void ContinueGame()
    {
        SaveManager.LoadToMemory();
        SceneManager.LoadScene(SaveManager.CurrentSave.sceneName);
    }

    public void StartNewGame()
    {
        SaveManager.DelteSave();
        SceneManager.LoadScene(FIRST_LEVEL_INDEX);
    }

    public void OptionsButton()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }
    
    public void CreditsButton()
    {
        mainMenuUI.SetActive(false);
        creditsMenuUI.SetActive(true);
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void BackToMainMenuButton()
    {
        optionsMenuUI.SetActive(false);
        creditsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
