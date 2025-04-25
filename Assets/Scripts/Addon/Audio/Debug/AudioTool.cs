using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTool : MonoBehaviour
{

    [Range(0, 100)]
    public float musicVolume;
    [Range(0, 100)]
    public float sfxVolume;
    public bool muteMusic, muteSFX;

    private void Start() => SetSetting();
    private void OnValidate() => SetSetting();
    private void SetSetting()
    {
        AudioSetting.MusicVolume = musicVolume;
        AudioSetting.SFXVolume = sfxVolume;

        AudioSetting.MusicMute = muteMusic;
        AudioSetting.SFXMute = muteSFX;
    }
}