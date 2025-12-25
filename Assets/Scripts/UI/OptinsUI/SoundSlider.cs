using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Scrollbar soundScrollbar;
    [SerializeField] private string exposedParam;

    private void OnEnable()
    {
        float currentValue = AudioManager.Instance.GetVolume(exposedParam);
        soundScrollbar.value = currentValue;
        soundScrollbar.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnDisable()
    {
        soundScrollbar.onValueChanged.RemoveListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float volume)
    {
        AudioManager.Instance.SetVolume(exposedParam, volume);
    }
}
