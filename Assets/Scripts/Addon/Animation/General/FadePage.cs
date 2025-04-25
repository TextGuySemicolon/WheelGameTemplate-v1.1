using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class FadePage : MonoBehaviour
{
    [SerializeField] private UnityEvent onShowEvent;
    [SerializeField] private UnityEvent onHideEvent;
    [SerializeField] private CanvasGroup group;

    public void ShowGroupDelay(float _delay)
    {
        LeanTween.delayedCall(gameObject, _delay, ShowGroup);
    }
    public void ShowGroup()
    {
        onShowEvent?.Invoke();

        LeanTween.delayedCall(gameObject, 1f, () => {
            group.interactable = true;
            group.blocksRaycasts = true;
        });


        group.alpha = 0f;
        LeanTween.value(gameObject, (float _alpha) =>
        {
            group.alpha = _alpha;
        }, 0f, 1f, 0.2f);
    }

    public void HideGroupDelay(float _delay)
    {
        LeanTween.delayedCall(gameObject, _delay, HideGroup);
    }
    public void HideGroup()
    {
        onHideEvent?.Invoke();

        group.interactable = false;
        group.blocksRaycasts = false;

        group.alpha = 1f;
        LeanTween.value(gameObject, (float _alpha) =>
        {
            group.alpha = _alpha;
        }, 1f, 0f, 0.2f).setDelay(0.1f);
    }


}
