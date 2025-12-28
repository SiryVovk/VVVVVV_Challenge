using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeRoutine : MonoBehaviour
{
    public bool IsFading => isFading;
    
    [SerializeField] private Image imageToFade;
    [SerializeField] private float defaultDuration = 1f;

    private bool isFading = false;

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        isFading = true;
        float elapsedTime = 0f;
        Color color = imageToFade.color;
        color.a = 0f;
        imageToFade.color = color;

        while (elapsedTime < defaultDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / defaultDuration);
            imageToFade.color = color;
            yield return null;
        }

        color.a = 1f;
        imageToFade.color = color;
        isFading = false;
    }

    public IEnumerator FadeOut()
    {
        isFading = true;
        float elapsedTime = 0f;
        Color color = imageToFade.color;
        color.a = 1f;
        imageToFade.color = color;

        while (elapsedTime < defaultDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsedTime / defaultDuration);
            imageToFade.color = color;
            yield return null;
        }

        color.a = 0f;
        imageToFade.color = color;
        isFading = false;
    }
}
