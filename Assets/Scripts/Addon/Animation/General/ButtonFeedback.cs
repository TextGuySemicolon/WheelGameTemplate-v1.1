using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Transform parent;
    [SerializeField] private LeanTweenType easeTypeDown, easeTypeUp;
    [SerializeField] private float scaleMultiplier, scaleDuration;

    private float initialSize;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (initialSize == 0f) initialSize = transform.localScale.x;
        LeanTween.cancel(parent.gameObject);
        LeanTween.scale(parent.gameObject, Vector3.one * scaleMultiplier, scaleDuration).setEase(easeTypeDown);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (initialSize == 0f) initialSize = transform.localScale.x;
        LeanTween.cancel(parent.gameObject);
        LeanTween.scale(parent.gameObject, Vector3.one * initialSize, scaleDuration).setEase(easeTypeUp);
    }

}
