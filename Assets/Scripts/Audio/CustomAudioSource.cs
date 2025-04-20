using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    /// <summary>
    /// Wraps Unity's AudioSource with custom behaviour and lifecycle management,
    /// including automatic recycling via coroutines.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class CustomAudioSource : MonoBehaviour
    {
        private AudioSource _self;
        
        public static List<CustomAudioSource> ActiveAudioSources = new();
        public Action<CustomAudioSource> OnRecycleRequested;

        private void OnEnable()
        {
            if (!ActiveAudioSources.Contains(this))
                ActiveAudioSources.Add(this);
        }

        private void OnDisable()
        {
            ActiveAudioSources.Remove(this);
        }

        private void ResetData()
        {
            // Stop Audio
            _self.Stop();
        }

        public void Init(AudioMixerGroup group)
        {
            if (!_self) _self = GetComponent<AudioSource>();

            _self.outputAudioMixerGroup = group;
        }

        /// <summary>
        /// Plays a clip once, and auto-resets/recycles after it's done.
        /// </summary>
        public void PlayOnce(AudioClip clip, float volume = 1f)
        {
            if (!_self) _self = GetComponent<AudioSource>();
            _self.volume = volume;
            _self.PlayOneShot(clip);
            StartCoroutine(ResetObject(clip.length + 0.5f));
        }

        /// <summary>
        /// Plays a clip in looping mode. You must manually stop it via StopAudio().
        /// </summary>
        public void PlayLooping(AudioClip clip, float volume = 1f)
        {
            if (!_self) _self = GetComponent<AudioSource>();
            _self.clip = clip;
            _self.volume = volume;
            _self.loop = true;
            _self.Play();
        }

        /// <summary>
        /// Stops playback and begins the reset/recycle coroutine immediately.
        /// </summary>
        public void StopAudio()
        {
            StartCoroutine(ResetObject(0f));
        }

        private IEnumerator ResetObject(float delay)
        {
            yield return new WaitForSeconds(delay);
            ResetData();

            gameObject.SetActive(false);
            OnRecycleRequested?.Invoke(this); // Hand it back to the pool
        }
    }
}