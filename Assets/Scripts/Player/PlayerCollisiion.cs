using System;
using UnityEngine;

public class PlayerCollisiion : MonoBehaviour, ILoadable
{
    [SerializeField] private SoundData deathSound;
    [SerializeField] private SoundData keyCollectSound;

    public Action OnPlayerDeath;
    public Action OnKeyCollected;

    public bool HasKey => hasKey;

    private bool hasKey = false;

    private const string OBSTICLE_TAG = "Obsticles";

    private void Awake()
    {
        LoadData();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(OBSTICLE_TAG))
        {
            OnPlayerDeath?.Invoke();
            SoundBuilder soundBuilder = SoundPool.Instance.CreateSoundBuilder()
                .WithSoundData(deathSound)
                .AtPosition(transform.position);

            soundBuilder.Play();
        }

        if(collision.gameObject.TryGetComponent(out Key key))
        {
            SoundBuilder soundBuilder = SoundPool.Instance.CreateSoundBuilder()
                .WithSoundData(keyCollectSound)
                .AtPosition(transform.position);
            soundBuilder.Play();
            hasKey = true;
            OnKeyCollected?.Invoke();
            Destroy(key.gameObject);
        }
    }

    public void LoadData()
    {
        if(SaveManager.CurrentSave != null && SaveManager.CurrentSave.isKeyCollected)
        {
            hasKey = true;
        }
    }
}
