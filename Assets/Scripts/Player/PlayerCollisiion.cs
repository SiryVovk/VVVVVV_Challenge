using System;
using UnityEngine;

public class PlayerCollisiion : MonoBehaviour, ILoadable
{
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
        }

        if(collision.gameObject.TryGetComponent(out Key key))
        {
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
