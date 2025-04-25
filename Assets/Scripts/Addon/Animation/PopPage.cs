using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class PopPage : MonoBehaviour
{
    [SerializeField] private float intervalDuration;
    [SerializeField] private float scaleDuration;
    [SerializeField] private LeanTweenType showTweenType, hideTweenType;
    [SerializeField] private GameObject[] items;

    [SerializeField] private UnityEvent onShowEvent;
    [SerializeField] private UnityEvent onHideEvent;

    public void ShowGroupDelay(float _delay)
    {
        LeanTween.delayedCall(gameObject, _delay, Show);
    }
    public void Show()
    {
        onShowEvent?.Invoke();

        float _interval = 0f;
        for(int i = 0; i < items.Length; i++)
        {
            LeanTween.cancel(items[i]);
            items[i].transform.localScale = Vector3.zero;
            LeanTween.scale(items[i], Vector3.one, scaleDuration).setEase(showTweenType).setDelay(_interval);
            _interval += intervalDuration;
        }
    }

    public void HideGroupDelay(float _delay)
    {
        LeanTween.delayedCall(gameObject, _delay, Hide);
    }
    public void Hide()
    {
        onHideEvent?.Invoke();

        float _interval = 0f;
        for (int i = 0; i < items.Length; i++)
        {
            LeanTween.cancel(items[i]);
            LeanTween.scale(items[i], Vector3.zero, scaleDuration).setEase(hideTweenType).setDelay(_interval);
            _interval += intervalDuration;
        }
    }
}
