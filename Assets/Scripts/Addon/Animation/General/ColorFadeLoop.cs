using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorFadeLoop : MonoBehaviour
{
    [SerializeField] private LeanTweenType easeType;
    [SerializeField] private float fadeDuration;
    [SerializeField] private Color fadeColor;

    private Color initialColor;
    private SpriteRenderer renderer;
    private void OnEnable() => Animate();
    private void Animate()
    {
        if(renderer == null)
        {
            renderer = GetComponent<SpriteRenderer>();
            initialColor = renderer.color;
        }

        LeanTween.cancel(gameObject);
        renderer.color = initialColor;
        LeanTween.value(gameObject, (float _value)=> {
            renderer.color = Color.Lerp(initialColor, fadeColor, _value);
        },0f, 1f, fadeDuration).setEase(easeType).setLoopPingPong();
    }
}
