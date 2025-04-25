using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class AudioSetting
{
    //Volume (music, sfx)
    public static float MusicVolume
    {
        get => PlayerPrefs.GetFloat("MusicVolume", 1f);

        set
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
            onSettingChanged?.Invoke();
        }
    }
    public static float SFXVolume
    {
        get => PlayerPrefs.GetFloat("SFXVolume", 1f);

        set
        {
            PlayerPrefs.SetFloat("SFXVolume", value);
            onSettingChanged?.Invoke();
        }
    }

    //Mute (music, sfx)
    public static bool MusicMute
    {
        get => PlayerPrefs.GetInt("MusicMute", 0) == 1;

        set
        {
            if (value == true)
            {
                PlayerPrefs.SetInt("MusicMute", 1);
            }
            else
            {
                PlayerPrefs.SetInt("MusicMute", 0);
            }
            onSettingChanged?.Invoke();
        }
    }
    public static bool SFXMute
    {
        get => PlayerPrefs.GetInt("SFXMute", 0) == 1;

        set
        {
            if (value == true)
            {
                PlayerPrefs.SetInt("SFXMute", 1);
            }
            else
            {
                PlayerPrefs.SetInt("SFXMute", 0);
            }
            onSettingChanged?.Invoke();
        }
    }

    public static Action onSettingChanged;
}
