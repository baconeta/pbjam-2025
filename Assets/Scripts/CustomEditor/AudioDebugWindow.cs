using System.Collections.Generic;
using System.Linq;

namespace CustomEditor
{
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEngine;
    using Audio;

    public class AudioDebugWindow : EditorWindow
    {
        [MenuItem("Window/Audio Debugger")]
        public static void ShowWindow()
        {
            GetWindow<AudioDebugWindow>("Audio Debugger");
        }

        private Vector2 _scroll;
        private int _selectedMixerGroupIndex = 0; // Store the index of the selected mixer group
        private string[] _mixerGroupOptions; // Store the options for the dropdown
        private bool _showOnlyPlaying = false;

        private void OnGUI()
        {
            EditorGUILayout.LabelField("🎧 Active CustomAudioSources", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // Get the options for the mixer groups
            _mixerGroupOptions = GetMixerGroupOptions();

            // Filter by Mixer Group
            EditorGUILayout.LabelField("Filter by Mixer Group");
            _selectedMixerGroupIndex = EditorGUILayout.Popup(_selectedMixerGroupIndex, _mixerGroupOptions);

            // Show only playing clips checkbox
            _showOnlyPlaying = EditorGUILayout.Toggle("Show Only Playing Clips", _showOnlyPlaying);

            EditorGUILayout.Space();

            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            // Get all sources (active and inactive)
            CustomAudioSource[] sources = FindObjectsByType<CustomAudioSource>(FindObjectsSortMode.None);

            if (sources.Length == 0)
            {
                EditorGUILayout.HelpBox("No active CustomAudioSources found.", MessageType.Info);
            }

            foreach (CustomAudioSource source in sources)
            {
                if (ShouldDisplaySource(source))
                {
                    DrawSourceInfo(source);
                }
            }

            EditorGUILayout.EndScrollView();

            Repaint(); // Auto-refresh
        }

        private string[] GetMixerGroupOptions()
        {
            // Fetch all available mixers in the project
            var mixerGroups =
                Managers.AudioManager.Instance.MasterMixer.FindMatchingGroups(string.Empty); // Get all groups

            var options = new List<string> { "All" };
            options.AddRange(from @group in mixerGroups where @group.name != "Master" select @group.name);

            return options.ToArray();
        }

        private bool ShouldDisplaySource(CustomAudioSource source)
        {
            AudioSource unitySource = source.GetComponent<AudioSource>();
            if (unitySource == null) return false;

            // Filter by mixer group
            if (_selectedMixerGroupIndex > 0 &&
                unitySource.outputAudioMixerGroup.name != _mixerGroupOptions[_selectedMixerGroupIndex])
            {
                return false;
            }

            // Filter by playing status
            if (_showOnlyPlaying && !unitySource.isPlaying)
            {
                return false;
            }

            return true;
        }

        private void DrawSourceInfo(CustomAudioSource source)
        {
            if (source == null) return;

            AudioSource unitySource = source.GetComponent<AudioSource>();
            if (unitySource == null) return;

            string clipName = unitySource.clip ? unitySource.clip.name : "(None)";
            string mixerGroup = unitySource.outputAudioMixerGroup ? unitySource.outputAudioMixerGroup.name : "(None)";
            string loop = unitySource.loop ? "🔁" : "⏯";
            string playing = unitySource.isPlaying ? "▶️" : "⏸";

            Color oldColor = GUI.color;
            GUI.color = unitySource.isPlaying ? Color.green : Color.gray;

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"{playing} {clipName} {loop}", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Mixer Group:", mixerGroup);
            EditorGUILayout.LabelField("GameObject:", source.gameObject.name);
            EditorGUILayout.LabelField("Scene:", source.gameObject.scene.name);
            EditorGUILayout.EndVertical();

            GUI.color = oldColor;
            EditorGUILayout.Space(4);
        }
    }
#endif
}