namespace CustomEditor
{
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class SingletonTrackerWindow : EditorWindow
    {
        [MenuItem("Tools/Singleton Tracker")]
        public static void ShowWindow()
        {
            GetWindow<SingletonTrackerWindow>("Singleton Tracker");
        }

        private Vector2 _scroll;

        private void OnGUI()
        {
            GUILayout.Label("Active Singleton Instances", EditorStyles.boldLabel);
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            foreach (var singleton in FindAllSingletons())
            {
                var type = singleton.GetType();
                string typeName = type.Name;
                Color boxColor = GetColorForType(type);

                // Draw a colored background
                Rect boxRect = EditorGUILayout.BeginVertical();
                EditorGUI.DrawRect(new Rect(boxRect.x, boxRect.y, boxRect.width, 68), boxColor * 0.15f);

                EditorGUILayout.LabelField("Type", type.FullName, EditorStyles.boldLabel);
                EditorGUILayout.ObjectField("Instance", singleton, typeof(UnityEngine.Object),
                    true);

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Ping"))
                    EditorGUIUtility.PingObject(singleton);

                if (GUILayout.Button("Reset"))
                {
                    MethodInfo reset = type.GetMethod("ResetInstance", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                    if (reset != null)
                        reset.Invoke(null, null);
                    else
                        Debug.LogWarning($"No ResetInstance() on {type.Name}");
                }

                GUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(8);
                EditorGUILayout.LabelField("Scene", GetSceneName(singleton));
            }

            EditorGUILayout.EndScrollView();
        }

        private IEnumerable<Component> FindAllSingletons()
        {
            foreach (var mono in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None))
            {
                var t = mono.GetType();

                if (IsSubclassOfRawGeneric(typeof(Utils.Singleton<>), t) ||
                    IsSubclassOfRawGeneric(typeof(Utils.EverlastingSingleton<>), t))
                {
                    yield return mono;
                }
            }
        }

        private static bool IsSubclassOfRawGeneric(Type rawGeneric, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(MonoBehaviour))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (cur == rawGeneric)
                    return true;

                toCheck = toCheck.BaseType;
            }

            return false;
        }

        private Color GetColorForType(Type type)
        {
            if (IsSubclassOfRawGeneric(typeof(Utils.EverlastingSingleton<>), type))
                return Color.green;

            if (IsSubclassOfRawGeneric(typeof(Utils.Singleton<>), type))
                return Color.cyan;

            return Color.gray;
        }

        [InitializeOnLoadMethod]
        private static void Init()
        {
            EditorApplication.playModeStateChanged += _ => RefreshAllWindows();
            AssemblyReloadEvents.afterAssemblyReload += RefreshAllWindows;
        }

        private static void RefreshAllWindows()
        {
            var windows = Resources.FindObjectsOfTypeAll<SingletonTrackerWindow>();
            foreach (var win in windows)
            {
                win.Repaint();
            }
        }

        private string GetSceneName(Component singleton)
        {
            var scene = singleton.gameObject.scene;
            return scene.IsValid() ? scene.name : "(Not in scene)";
        }
    }
#endif
}