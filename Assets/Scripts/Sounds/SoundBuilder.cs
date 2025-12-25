using UnityEngine;

public class SoundBuilder
{
    private readonly SoundPool soundPool;
    private SoundData soundData;
    private Vector3 position = Vector3.zero;
    private bool randomizePitch = false;

    public SoundBuilder(SoundPool soundPool)
    {
        this.soundPool = soundPool;
    }
    
    public SoundBuilder WithSoundData(SoundData soundData)
    {
        this.soundData = soundData;
        return this;
    }

    public SoundBuilder AtPosition(Vector3 position)
    {
        this.position = position;
        return this;
    }

    public SoundBuilder WithRandomizedPitch()
    {
        this.randomizePitch = true;
        return this;
    }

    public SoundEmiter Play(Transform parent = null)
    {
        SoundEmiter soundEmmiter = soundPool.Get();
        soundEmmiter.Initilize(soundData);
        soundEmmiter.transform.position = position;

        soundEmmiter.transform.SetParent(SoundPool.Instance.transform, false);

        if (randomizePitch)
        {
            soundEmmiter.WithEandomPitch();
        }

        soundEmmiter.PlaySound();

        return soundEmmiter;
    }
}
