using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JYAudio;
using AudioType = JYAudio.AudioType;

public class AudioSwapper : MonoBehaviour
{
    [SerializeField] private AudioRunner runner;

    public void Set(AudioClip _clip)
    {
        runner.Init(new AudioInfo(_clip, AudioType.Music, runner.volume, Vector2.zero, true));
        runner.PlayAudio();
    }

    public void FadeOut()
    {
        LeanTween.value(gameObject, (float _value) =>
        {
            AudioSetting.MusicVolume = _value;
        }, 1f, 0f, 0.2f);
    }

    public void FadeIn()
    {
        LeanTween.value(gameObject, (float _value) =>
        {
            AudioSetting.MusicVolume = _value;
        }, 0f, 1f, 0.2f);
    }

}
