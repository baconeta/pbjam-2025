using Managers;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class TestAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip testMusic;
        [SerializeField] private AudioClip testSfx;
        [SerializeField] private AudioClip testAmbient;

        [SerializeField] private AudioMixerGroup musicMixerGroup;
        [SerializeField] private AudioMixerGroup sfxMixerGroup;
        [SerializeField] private AudioMixerGroup ambientMixerGroup;

        [SerializeField] private AudioManager audioManager;
        [SerializeField] private AudioClip[] clipsToBuild;

        private CustomAudioSource _music;
        private CustomAudioSource _ambient;
        private CustomAudioSource _sfx;
        [SerializeField] private AudioBuilderSystem audioBuilder;

        public void TestMusicLooping()
        {
            if (_music) _music.StopAudio();
            _music = audioManager.Play(testMusic, musicMixerGroup);
        }

        public void TestMusicOnce()
        {
            if (_music) _music.StopAudio();
            _music = audioManager.Play(testMusic, musicMixerGroup, false);
        }

        public void TestSfxLooping()
        {
            _sfx = audioManager.Play(testSfx, sfxMixerGroup);
        }

        public void TestSfxOnce()
        {
            audioManager.Play(testSfx, sfxMixerGroup, false);
        }

        public void TestAmbientLooping()
        {
            if (_ambient) _ambient.StopAudio();
            _ambient = audioManager.Play(testAmbient, ambientMixerGroup);
        }

        public void TestAmbientOnce()
        {
            if (_ambient) _ambient.StopAudio();
            audioManager.Play(testAmbient, ambientMixerGroup, false);
        }

        public void StopSfx()
        {
            if (_sfx && _sfx.isActiveAndEnabled) _sfx.StopAudio();
            _sfx = null;
        }

        public void StopMusic()
        {
            if (_music && _music.isActiveAndEnabled) _music.StopAudio();
            _music = null;
        }

        public void StopAmbient()
        {
            if (_ambient && _ambient.isActiveAndEnabled) _ambient.StopAudio();
            _ambient = null;
        }


        public void ButtonAddTracks()
        {
            foreach (var clip in clipsToBuild)
            {
                clip.LoadAudioData();
                audioBuilder.AddClipToBuilder(clip);
            }
        }

        public void ButtonPlayTracks()
        {
            audioBuilder.PlayBuiltClips();
        }
    }
}