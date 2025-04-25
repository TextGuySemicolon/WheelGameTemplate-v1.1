using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JYAudio;
using AudioType = JYAudio.AudioType;

[System.Serializable]
public class AudioEvent
{
    public AudioClip clip;
    public AudioType type; //sfx or music
    public float volume = 1f;
    public Vector2 pitchRange = new Vector2(1f, 1f);
    public bool ignoreTimescale;
    public bool loop;
    public float delay;

    public AudioClip[] alternativeClips;

    private AudioRunner runner;

    public AudioInfo GetInfo() => new AudioInfo(clip, type, volume, pitchRange, loop, ignoreTimescale, alternativeClips);
    public bool IsPlaying() => runner;
    public void Play()
    {
        if (runner != null) GameObject.Destroy(runner.gameObject);
        AudioInfo _info = new AudioInfo(clip, type, volume, pitchRange, loop, ignoreTimescale, alternativeClips);
        if (delay > 0f)
        {
            runner = AudioSystem.CreateAudioRunner(_info).PlayAudioDelay(delay);
        }
        else
        {
            runner = AudioSystem.CreateAudioRunner(_info).PlayAudio();
        }
    }
    public void Stop()
    {
        if (!runner) return;
        GameObject.Destroy(runner.gameObject);
    }
}
public static class AudioEventExtensions
{
    public static void TryPlay(this AudioEvent[] _audioEvents)
    {
        foreach (AudioEvent _audio in _audioEvents)
        {
            if (!_audio.IsPlaying()) _audio.Play();
        }
    }
    public static void Play(this AudioEvent[] _audioEvents)
    {
        foreach (AudioEvent _audio in _audioEvents)
        {
            _audio.Play();
        }
    }
    public static void Stop(this AudioEvent[] _audioEvents)
    {
        foreach (AudioEvent _audio in _audioEvents)
        {
            _audio.Stop();
        }
    }
}