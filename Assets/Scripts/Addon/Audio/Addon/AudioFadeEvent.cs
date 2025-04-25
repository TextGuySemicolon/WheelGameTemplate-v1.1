using UnityEngine;
using JYAudio;

public class AudioFadeEvent : MonoBehaviour
{
    public AudioRunner runner;
    public float fadeDuration;
    public void FadeVolume(float _volume)
    {
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, (_value) =>
        {
            runner.volume = _value;
            runner.UpdateSource();
        }, runner.volume, _volume, fadeDuration).setEaseOutCirc();
    }
}