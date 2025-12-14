using System;
using UnityEngine;

public class PlayerCollisiion : MonoBehaviour
{
    public Action OnPlayerDeath;

    private const string OBSTICLE_TAG = "Obsticles";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(OBSTICLE_TAG))
        {
            OnPlayerDeath?.Invoke();
        }
    }
}
