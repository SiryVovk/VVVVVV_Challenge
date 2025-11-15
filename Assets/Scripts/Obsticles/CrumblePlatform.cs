using UnityEngine;
using System.Collections;
using System;

public class CrumblePlatform : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [SerializeField] private float crumblDeley = 0.2f;
    [SerializeField] private float fadeTime = 0.4f;
    [SerializeField] private float respawnTime = 3f;

    private Collider2D platformCollider;

    private bool isCrumbling;
    private bool isCrumbeled;

    private const string CRUMBL_TRIGER = "Crumbl";
    private const string UNCRUMBL_TRIGER = "UnCrumbl";

    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isCrumbling || isCrumbeled) 
        {
            return;
        }

        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            StartCoroutine(CrumblSequanceRoutine());
        }
    }

    private IEnumerator CrumblSequanceRoutine()
    {
        isCrumbling = true;
        yield return new WaitForSeconds(crumblDeley);

        if (animator)
        {
            animator.SetTrigger(CRUMBL_TRIGER);
        }
    }

    public void OnCrumblAnimationEvent()
    {
        platformCollider.enabled = false;
        StartCoroutine(FadeRoutine(0f));
    }

    private IEnumerator FadeRoutine(float targetAlpfa)
    {
        Color targetColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, targetAlpfa);
        Color startColor = spriteRenderer.color;

        float time = 0f;

        while(time < fadeTime)
        {
            time += Time.deltaTime;
            float lerpTime = time / fadeTime;
            spriteRenderer.color = Color.Lerp(startColor, targetColor, lerpTime);
            
            yield return null;
        }

        spriteRenderer.color = targetColor;

        CrumblingOrRespawn(targetAlpfa);

        if(targetAlpfa == 0f)
        {
            RespawnStart();
        }
        
    }

    private void CrumblingOrRespawn(float targetAlpfa)
    {
        if(targetAlpfa == 0f)
        {
            isCrumbeled = true;
        }
        else
        {
            isCrumbeled = false;
            isCrumbling = false;
            platformCollider.enabled = true;
        }
    }

    private void RespawnStart()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnTime);

        if (animator)
        {
            animator.SetTrigger(UNCRUMBL_TRIGER);
        }

        StartCoroutine(FadeRoutine(1f));

    }
}
