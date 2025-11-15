using UnityEngine;

public class CrumblingPlatformVisual : MonoBehaviour
{
    [SerializeField] private CrumblePlatform parentPlatform;

    // викликається з Animation Event
    public void AnimationEvent_Break()
    {
        parentPlatform.OnCrumblAnimationEvent();
    }
}
