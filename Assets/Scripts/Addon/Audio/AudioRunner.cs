using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JYAudio
{

    public enum AudioType { SFX, Music }
    [RequireComponent(typeof(AudioSource))]
    public class AudioRunner : MonoBehaviour
    {
        [SerializeField] private AudioType type;
        public float volume = 1f;
        public bool ignoreTimescale;
        private AudioSource ac;

        private void Awake()
        {
            ac = GetComponent<AudioSource>();
            UpdateSource();
        }
        #region Enable & Disable
        private void OnEnable()
        {
            AudioSetting.onSettingChanged += UpdateSource;
        }
        private void OnDisable()
        {
            AudioSetting.onSettingChanged -= UpdateSource;
        }
        #endregion

        /// <summary>
        /// apply audio info to audio source
        /// </summary>
        public void Init(AudioInfo _info)
        {
            type = _info.type;
            volume = _info.volume;
            ignoreTimescale = _info.ignoreTimescale;

            AudioSource _ac = GetComponent<AudioSource>();
            _ac.clip = _info.GetClip();
            _ac.loop = _info.loop;
            _ac.pitch = Random.Range(_info.pitchRange.x, _info.pitchRange.y);

            UpdateSource();
        }

        /// <summary>
        /// apply current volume to the audio source
        /// </summary>
        public void UpdateSource()
        {
            if (ac == null) return;

            switch (type)
            {
                case AudioType.Music:
                    ac.volume = AudioSetting.MusicMute ? 0f : volume * AudioSetting.MusicVolume;
                    break;
                case AudioType.SFX:
                    ac.volume = AudioSetting.SFXMute ? 0f : volume * AudioSetting.SFXVolume;
                    break;
            }
        }
        /// <summary>
        /// slow down audio based on time scale
        /// </summary>
        private void Update()
        {
            if (!ignoreTimescale) ac.pitch = Time.timeScale;
        }
    }

    public struct AudioInfo
    {
        public AudioClip clip;
        public AudioType type;
        public float volume;
        public Vector2 pitchRange;
        public bool loop;
        public bool ignoreTimescale;

        public AudioClip[] alternativeClips;

        public AudioInfo(AudioClip _clip, AudioType _type, float _volume = 1f, Vector2 _pitchRange = default, bool _loop = false, bool _ignoreTimescale = false, AudioClip[] _alternativeClips = null)
        {
            clip = _clip;
            type = _type;
            volume = _volume;
            pitchRange = _pitchRange;
            loop = _loop;
            ignoreTimescale = _ignoreTimescale;
            alternativeClips = _alternativeClips;
        }

        public AudioClip GetClip()
        {
            if (alternativeClips == null) return clip;
            List<AudioClip> _randomList = new List<AudioClip>(alternativeClips);
            _randomList.Add(clip);
            return _randomList[Random.Range(0, _randomList.Count)];
        }
    }
}