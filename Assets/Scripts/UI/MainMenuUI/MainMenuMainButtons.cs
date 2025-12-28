using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuMainButtons : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject optionsMenuUI;
    [SerializeField] private Button continueButton;
    [SerializeField] private SoundData buttonSound;
    [SerializeField] private FadeRoutine fadeRoutine;

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
        int levelIndex = SceneUtility.GetBuildIndexByScenePath(SaveManager.CurrentSave.sceneName);
        StartCoroutine(StartRoutine(levelIndex));
    }

    public void StartNewGame()
    {
        PlayButtonSound();
        SaveManager.DelteSave();
        StartCoroutine(StartRoutine(FIRST_LEVEL_INDEX));
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

    private IEnumerator StartRoutine(int index)
    {
        yield return fadeRoutine.FadeIn();
        yield return new WaitUntil(() => !fadeRoutine.IsFading);

        SceneManager.LoadScene(index);
    }
}
