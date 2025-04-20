using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        /// <summary>
        /// Basic singleton class for Unity MonoBehaviours.
        /// Automatically finds or creates the instance when accessed.
        /// This version does NOT persist across scenes.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        GameObject newInstance = new(typeof(T).Name);
                        _instance = newInstance.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (!Application.isPlaying)
                return;

            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Editor/debug-only method to reset the singleton instance manually.
        /// Useful when reloading scenes or testing.
        /// </summary>
        public static void ResetInstance()
        {
            _instance = null;
        }
#endif
    }
}