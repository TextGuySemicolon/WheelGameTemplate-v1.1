using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JYAudio
{
    public static class AudioSystem
    {
        /// <summary>
        /// spawn an audio runner gameobject
        /// </summary>
        /// <param name="_info"></param>
        /// <returns></returns>
        public static AudioRunner CreateAudioRunner(AudioInfo _info)
        {
            GameObject _audioRunner = new GameObject();
            _audioRunner.name = "AudioRunner" + $"({_info.clip.name})";
            _audioRunner.AddComponent<AudioSource>();
            _audioRunner.AddComponent<AudioRunner>().Init(_info);
            return _audioRunner.GetComponent<AudioRunner>();
        }

        #region Play Audio
        /// <summary>
        /// play audio on audio runner with delay
        /// </summary>
        /// <param name="_audioRunner"></param>
        /// <param name="_delay"></param>
        /// <returns></returns>
        public static AudioRunner PlayAudioDelay(this AudioRunner _audioRunner, float _delay)
        {
            LeanTween.delayedCall(_audioRunner.gameObject, _delay, () =>
            {
                _audioRunner.PlayAudio();
            });
            return _audioRunner;
        }
        /// <summary>
        /// play audio on audio runner
        /// </summary>
        /// <param name="_audioRunner"></param>
        /// <returns></returns>
        public static AudioRunner PlayAudio(this AudioRunner _audioRunner)
        {
            AudioSource _ac = _audioRunner.GetComponent<AudioSource>();
            _ac.Play();

            if (!_ac.loop) GameObject.Destroy(_audioRunner.gameObject, _ac.clip.length);

            return _audioRunner;
        }
        #endregion
    }

}