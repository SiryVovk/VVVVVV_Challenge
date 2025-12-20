using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPoiint : MonoBehaviour, ILoadable
{
    [SerializeField] private GameObject activeExitPointSprite;
    [SerializeField] private GameObject deactiveExitPointSprite;
    [SerializeField] private PlayerCollisiion playerCollision;
    [SerializeField] private SoundData exitSound;

    [SerializeField] private int nextLevelIndex;

    private void Awake()
    {
        LoadData();
    }
    
    private void OnEnable()
    {
        playerCollision.OnKeyCollected += HandleKeyCollected;
    }

    private void OnDisable()
    {
        playerCollision.OnKeyCollected -= HandleKeyCollected;
    }

    private void HandleKeyCollected()
    {
        activeExitPointSprite.SetActive(true);
        deactiveExitPointSprite.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerCollisiion playerCollision))
        {
            if(playerCollision.HasKey)
            {
                SoundBuilder soundBuilder = SoundPool.Instance.CreateSoundBuilder()
                    .WithSoundData(exitSound)
                    .AtPosition(transform.position);
                soundBuilder.Play();
                SceneManager.LoadScene(nextLevelIndex);
            }
        }
    }

    public void LoadData()
    {
        if(SaveManager.CurrentSave != null && SaveManager.CurrentSave.isKeyCollected)
        {
            activeExitPointSprite.SetActive(true);
            deactiveExitPointSprite.SetActive(false);
        }
    }
}