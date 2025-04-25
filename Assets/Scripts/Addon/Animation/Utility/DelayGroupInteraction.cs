using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayGroupInteraction : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private CanvasGroup group;
    public void DelayGroup()
    {
        group.interactable = false;
        group.blocksRaycasts = false;

        LeanTween.delayedCall(gameObject, delay, () => {
            group.interactable = true;
            group.blocksRaycasts = true;
        });
    }

}
