using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SoundPool : MonoBehaviour
{
    public static SoundPool Instance => instance;

    [SerializeField] private SoundEmiter soundEmiterPrefab;
    
    [SerializeField] private int poolSize = 10;
    [SerializeField] private int maxPoolSize = 50;

    private static SoundPool instance;
    private IObjectPool<SoundEmiter> soundsPool;
    private readonly List<SoundEmiter> activeEmitters = new List<SoundEmiter>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializePool();
    }

    private void InitializePool()
    {
        soundsPool = new ObjectPool<SoundEmiter>(
            CreateSoundEmitter,
            OnGetSoundEmitter,
            OnReleaseSoundEmitter,
            OnDestroySoundEmitter,
            false,
            poolSize,
            maxPoolSize
        );
    }

    private SoundEmiter CreateSoundEmitter()
    {
        var soundEmitter = Instantiate(soundEmiterPrefab);
        soundEmitter.gameObject.SetActive(false);
        return soundEmitter;
    }

    public SoundBuilder CreateSoundBuilder()
    {
        return new SoundBuilder(this);
    }

    private void OnGetSoundEmitter(SoundEmiter emitter)
    {
        emitter.gameObject.SetActive(true);
        activeEmitters.Add(emitter);
    }

    public SoundEmiter Get()
    {
        return soundsPool.Get();
    }

    private void OnReleaseSoundEmitter(SoundEmiter emitter)
    {
        emitter.gameObject.SetActive(false);
        activeEmitters.Remove(emitter);
    }

    public void Release(SoundEmiter emitter)
    {
        soundsPool.Release(emitter);
    }

    private void OnDestroySoundEmitter(SoundEmiter emitter)
    {
        if(emitter == null)
        {
            return;
        }
        Destroy(emitter.gameObject);
    }




}
