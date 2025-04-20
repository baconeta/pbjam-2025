namespace Objects
{
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;

    /// <summary>
    /// Manages a pool of objects of the same type. 
    /// Reuses objects when needed, instead of creating and destroying them repeatedly.
    /// </summary>
    /// <typeparam name="T">The type of object to pool. Must be a subclass of MonoBehaviour.</typeparam>
    public class ObjectPool<T> : IPoolDebugInfo where T : MonoBehaviour
    {
        private readonly GameObject _prefab;
        private readonly Queue<T> _objects = new();
        private readonly List<T> _allInstances = new();

        public ObjectPool(GameObject prefab, int initialSize)
        {
            _prefab = prefab;
            Expand(initialSize);
        }

        public T Get()
        {
            if (_objects.Count == 0) Expand(1);

            T instance = _objects.Dequeue();
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            _objects.Enqueue(obj);
        }

        private void Expand(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject obj = Object.Instantiate(_prefab);
                obj.SetActive(false);
                T component = obj.GetComponent<T>();
                _objects.Enqueue(component);
                _allInstances.Add(component);
            }
        }

        // IPoolDebugInfo implementation
        public string PoolName => typeof(T).Name;
        public GameObject Prefab => _prefab;
        public int TotalCount => _allInstances.Count;
        public int ActiveCount => _allInstances.Count(o => o.gameObject.activeSelf);

#if UNITY_EDITOR
        public void EditorSpawnOne()
        {
            Get();
        }

        public void EditorReturnAll()
        {
            foreach (var instance in _allInstances.Where(instance => instance.gameObject.activeInHierarchy))
            {
                Return(instance);
            }
        }

        public void EditorClear()
        {
            foreach (var obj in _allInstances.Where(obj => obj != null))
            {
                Object.DestroyImmediate(obj.gameObject);
            }

            _objects.Clear();
            _allInstances.Clear();
        }
#endif
    }
}