using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateScaleLoop : MonoBehaviour
{
    [SerializeField] private LeanTweenType easeType;
    [SerializeField] private float scaleMultiplier, scaleDuration;

    private float initialSize;
    private void OnEnable() => Animate();
    private void Animate()
    {
        if (initialSize == 0f) initialSize = transform.localScale.x;
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one * initialSize;
        LeanTween.scale(gameObject, Vector3.one * scaleMultiplier, scaleDuration).setEase(easeType).setLoopPingPong();
    }
}
