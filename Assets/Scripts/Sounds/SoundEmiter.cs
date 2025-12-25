using System.Collections;
using UnityEngine;

public class SoundEmiter : MonoBehaviour
{
    public SoundData SoundData { get; private set; }
    private AudioSource audioSource;
    private Coroutine playingRoutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void PlaySound()
    {
        if (playingRoutine != null)
        {
            StopCoroutine(playingRoutine);
        }
        
        audioSource.Play();

        if(SoundData.autoRelease)
        {
            playingRoutine = StartCoroutine(PlaySoundRoutine());
        }
    }

    public void StopSound()
    {
        if (playingRoutine != null)
        {
            StopCoroutine(playingRoutine);
            playingRoutine = null;
        }
        
        transform.parent = SoundPool.Instance.transform;
        audioSource.Stop();
        SoundPool.Instance.Release(this);
    }

    private IEnumerator PlaySoundRoutine()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        SoundPool.Instance.Release(this);
    }

    public void Initilize(SoundData soundData)
    {
        SoundData = soundData;
        audioSource.clip = soundData.soundClip;
        audioSource.outputAudioMixerGroup = soundData.mixerGroup;
        audioSource.loop = soundData.loop;
        audioSource.playOnAwake = soundData.playOnAwake;
    }

    public void WithEandomPitch(float min = -0.05f, float max = 0.05f)
    {
        audioSource.pitch += UnityEngine.Random.Range(min, max);
    }
}
