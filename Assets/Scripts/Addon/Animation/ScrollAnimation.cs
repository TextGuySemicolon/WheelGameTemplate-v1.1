using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAnimation : MonoBehaviour
{
    [SerializeField] private float scrollWidth;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private RectTransform rect;
    private void OnEnable() => Animate();
    private void Animate()
    {
        if (rect == null)
        {
            rect = GetComponent<RectTransform>();
        }

        LeanTween.cancel(rect.gameObject);
        LeanTween.value(rect.gameObject, (float _value) => {
            rect.anchoredPosition = new Vector3(_value, rect.anchoredPosition.y);
        }, 0f, rect.anchoredPosition.x + scrollWidth, 3f / scrollSpeed).setLoopClamp();
    }
}
