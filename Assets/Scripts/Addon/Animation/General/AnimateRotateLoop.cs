using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateRotateLoop : MonoBehaviour
{
    [SerializeField] private LeanTweenType easeType;
    [SerializeField] private float rotateDuration;
    private void OnEnable() => Animate();
    private void Animate()
    {
        LeanTween.cancel(gameObject);
        LeanTween.rotateAroundLocal(gameObject, Vector3.forward, 360f, rotateDuration).setEase(easeType).setLoopClamp();
    }
}
