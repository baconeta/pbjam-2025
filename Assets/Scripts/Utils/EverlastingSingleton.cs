using UnityEngine;

namespace Utils
{
    public class EverlastingSingleton<T> : MonoBehaviour where T : Component
    {
        /// <summary>
        /// Singleton that persists across scene loads.
        /// Use this for managers or services you want to keep alive globally.
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
                        DontDestroyOnLoad(newInstance);
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
                DontDestroyOnLoad(this);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}