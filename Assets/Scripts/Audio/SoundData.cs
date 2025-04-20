using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    /// <summary>
    /// Stores data about an individual sound, including the clip,
    /// mixer group, loop settings, and volume.
    /// Used by AudioWrapper to build the sound dictionary.
    /// </summary>
    [Serializable]
    public struct SoundData
    {
        [Tooltip("The name used to reference this sound via AudioWrapper.")] public string name;
        public AudioClip sound;
        public AudioMixerGroup mixer;
        public bool loop;
        [Range(0.1f, 1.0f)] public float volume;
    }
}