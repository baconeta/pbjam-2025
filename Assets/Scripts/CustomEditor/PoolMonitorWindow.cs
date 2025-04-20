using Objects;

namespace CustomEditor
{
    using UnityEditor;
    using UnityEngine;

    public class PoolMonitorWindow : EditorWindow
    {
        [MenuItem("Tools/Pool Monitor")]
        public static void ShowWindow()
        {
            GetWindow<PoolMonitorWindow>("Pool Monitor");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Live Object Pools", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            foreach (var pool in ObjectPoolManager.Instance.GetAllDebugPools())
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("Pool: " + pool.PoolName, EditorStyles.boldLabel);
                EditorGUILayout.ObjectField("Prefab", pool.Prefab, typeof(GameObject), false);
                EditorGUILayout.LabelField("Total Objects", pool.TotalCount.ToString());
                EditorGUILayout.LabelField("Active Objects", pool.ActiveCount.ToString());

                float usage = pool.TotalCount > 0 ? (float)pool.ActiveCount / pool.TotalCount : 0f;
                Rect progressRect = EditorGUILayout.GetControlRect();
                EditorGUI.ProgressBar(progressRect, usage, $"{pool.ActiveCount}/{pool.TotalCount} active");

                // Buttons
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Spawn One"))
                {
                    pool.EditorSpawnOne();
                }

                if (GUILayout.Button("Return All"))
                {
                    pool.EditorReturnAll();
                }

                if (GUILayout.Button("Clear"))
                {
                    pool.EditorClear();
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }

            if (Application.isPlaying)
                Repaint(); // live refresh while in play mode
        }
    }
}