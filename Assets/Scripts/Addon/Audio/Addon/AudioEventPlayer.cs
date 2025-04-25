using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventPlayer : MonoBehaviour
{
    [SerializeField] private bool playOnStart;
    [SerializeField] private AudioEvent[] audioEvents;

    private void OnEnable()
    {
        if (playOnStart) Play();
    }
    public void Play()
    {
        audioEvents.Play();
    }
}
