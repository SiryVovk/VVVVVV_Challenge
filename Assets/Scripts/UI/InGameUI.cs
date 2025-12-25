using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private UIInputHandler uIInputHandler;
    [SerializeField] private GameObject stopMenu;

    private bool isGamePaussed = false;
    
    private void OnEnable()
    {
        uIInputHandler.Escape += EscapeHandler;
    }

    private void OnDisable()
    {
        uIInputHandler.Escape -= EscapeHandler;
    }

    private void EscapeHandler()
    {
        if(!isGamePaussed)
        {
            Time.timeScale = 0f;
            stopMenu.SetActive(true); 
            isGamePaussed = true;
        }
        else
        {
            Time.timeScale = 1f;
            stopMenu.SetActive(false);
            isGamePaussed = false;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        stopMenu.SetActive(false);
        isGamePaussed = false;
    }

    public void ToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
