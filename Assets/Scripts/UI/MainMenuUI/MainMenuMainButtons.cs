using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuMainButtons : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject optionsMenuUI;
    [SerializeField] private Button continueButton;
    [SerializeField] private SoundData buttonSound;

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
        PlayButtonSound();
        SaveManager.LoadToMemory();
        SceneManager.LoadScene(SaveManager.CurrentSave.sceneName);
    }

    public void StartNewGame()
    {
        PlayButtonSound();
        SaveManager.DelteSave();
        SceneManager.LoadScene(FIRST_LEVEL_INDEX);
    }

    public void OptionsButton()
    {
        PlayButtonSound();
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }
    
    public void ExitButton()
    {
        PlayButtonSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void BackToMainMenuButton()
    {
        PlayButtonSound();
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    private void PlayButtonSound()
    {
        SoundBuilder soundBuilder = SoundPool.Instance.CreateSoundBuilder()
            .WithSoundData(buttonSound)
            .AtPosition(Vector3.zero);
        soundBuilder.Play();
    }
}
