using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMainButtons : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject optionsMenuUI;
    [SerializeField] private GameObject creditsMenuUI;

    private const int FIRST_LEVEL_INDEX = 1;

    public void ContinueGame()
    {
        throw new System.NotImplementedException();
    }

    public void StartNewGame()
    {
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
