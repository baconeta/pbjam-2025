using System.Collections.Generic;
using Audio;
using UI.Settings;
using UnityEngine;
using UnityEngine.Audio;
using Utils;

namespace Managers
{
    /// <summary>
    /// The AudioManager is responsible for creating and playing audio sources,
    /// hooked up to the appropriate mixer groups. It supports looping and one-shot playback.
    ///
    /// Use AudioManager.Instance.Play(...) to play audio directly,
    /// or use AudioWrapper if you want to play sounds by name.
    /// </summary>
    public sealed class AudioManager : EverlastingSingleton<AudioManager>
    {
        [SerializeField] private GameObject audioSourceObject;

        [Header("Mixers")] [SerializeField] private AudioMixer masterMixer;

        public const string MusicKey = "MusicVolume";
        public const string SfxKey = "SfxVolume";
        public const string AmbientKey = "AmbientVolume";
        private const float VolumeMultiplier = 20;

        private readonly Queue<CustomAudioSource> _pooledSources = new();
        private GameObject _audioPool;

        public AudioMixer MasterMixer => masterMixer;

        /// <summary>
        /// Plays a sound clip either once or looping.
        /// If no preset CustomAudioSource is provided, one will be instantiated (or pulled from pool if pooling is enabled).
        /// </summary>
        /// <param name="clip">The audio clip to play.</param>
        /// <param name="mixerGroup">The mixer group to route the audio through.</param>
        /// <param name="looping">Should the clip loop?</param>
        /// <param name="presetAudioSource">Optional. A specific CustomAudioSource to use (e.g. for UI sounds).</param>
        /// <returns>A reference to the CustomAudioSource playing the sound.</returns>
        public CustomAudioSource Play(AudioClip clip, AudioMixerGroup mixerGroup, bool looping = true, CustomAudioSource presetAudioSource = null)
        {
            CustomAudioSource audioSource = presetAudioSource ? presetAudioSource : Setup(mixerGroup, looping);

            if (looping)
            {
                audioSource.PlayLooping(clip);
            }
            else
            {
                audioSource.PlayOnce(clip);
            }

            return audioSource;
        }

        /// <summary>
        /// Prepares a new or pooled CustomAudioSource using the given mixer group.
        /// </summary>
        public CustomAudioSource Setup(AudioMixerGroup mixerGroup, bool looping = true)
        {
            if (audioSourceObject == null)
            {
                Debug.LogError("No custom object for audio");
                return null;
            }

            CustomAudioSource audioSource;

            if (_pooledSources.Count > 0)
            {
                audioSource = _pooledSources.Dequeue();
                audioSource.gameObject.SetActive(true);
            }
            else
            {
                GameObject gO = Instantiate(audioSourceObject, _audioPool.transform);
                audioSource = gO.AddComponent<CustomAudioSource>();
            }

            audioSource.Init(mixerGroup);
            audioSource.OnRecycleRequested = RecycleSource;
            return audioSource;
        }

        protected override void Awake()
        {
            base.Awake();
            LoadVolumes();
            
            if (_audioPool == null)
            {
                _audioPool = new GameObject("AudioPool");
                DontDestroyOnLoad(_audioPool);
            }
        }

        private static float SafeLogVolume(float value) => Mathf.Log10(Mathf.Max(value, 0.0001f)) * VolumeMultiplier;

        /// <summary>
        /// Loads saved volume settings from PlayerPrefs and applies them to the audio mixer.
        /// </summary>
        private void LoadVolumes() // Volume is saved in VolumeSettings.cs
        {
            float musicVol = PlayerPrefs.GetFloat(MusicKey, 0.5f);
            float sfxVol = PlayerPrefs.GetFloat(SfxKey, 0.5f);
            float ambientVol = PlayerPrefs.GetFloat(AmbientKey, 0.5f);

            MasterMixer.SetFloat(VolumeSettings.MixerMusic, SafeLogVolume(musicVol));
            MasterMixer.SetFloat(VolumeSettings.SfxMusic, SafeLogVolume(sfxVol));
            MasterMixer.SetFloat(VolumeSettings.AmbientMusic, SafeLogVolume(ambientVol));
        }
        
        private void RecycleSource(CustomAudioSource source)
        {
            _pooledSources.Enqueue(source);
        }
        
        public void StopAllAudio()
        {
            foreach (var source in _audioPool.GetComponentsInChildren<CustomAudioSource>())
            {
                source.StopAudio();
            }
        }
    }
}