using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}

    [Header("References")]
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private SoundData musicSoundData;

    private readonly string[] VOLUME_PARAMS = { "MasterVolume", "MusicVolume", "SFXVolume" };
    private SoundEmiter musicEmiter;
    
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SoundBuilder soundBuilder = SoundPool.Instance.CreateSoundBuilder()
                .WithSoundData(musicSoundData)
                .AtPosition(transform.position);
        musicEmiter = soundBuilder.Play();
        LoadAllVolume();
    }

    private void LoadAllVolume()
    {
        foreach(var param in VOLUME_PARAMS)
        {
            float volume = PlayerPrefs.GetFloat(param,1f);
            SetVolume(param, volume);
        }   
    }

     public void SetVolume(string exposedParam, float value01)
    {
        float volume = Mathf.Log10(Mathf.Clamp(value01, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat(exposedParam, volume);
        PlayerPrefs.SetFloat(exposedParam, value01);
    }

    public float GetVolume(string exposedParam)
    {
        return PlayerPrefs.GetFloat(exposedParam, 1f);
    }

}
