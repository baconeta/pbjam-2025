namespace Objects
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Manages a pool of objects of the same type. 
    /// Reuses objects when needed, instead of creating and destroying them repeatedly.
    /// </summary>
    /// <typeparam name="T">The type of object to pool. Must be a subclass of MonoBehaviour.</typeparam>
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _objectPool = new();  // Queue to store pooled objects
        private readonly T _prefab;  // The prefab of the object to pool
        private readonly Transform _parent;  // Parent transform for instantiated objects
        
        /// <summary>
        /// Constructor to initialize the pool with a given prefab and initial size.
        /// </summary>
        /// <param name="prefab">The prefab of the object to pool.</param>
        /// <param name="initialSize">The initial number of objects to instantiate and add to the pool.</param>
        /// <param name="parent">The parent transform to instantiate the objects under.</param>
        public ObjectPool(T prefab, int initialSize, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;

            // Pre-instantiate objects to populate the pool
            for (int i = 0; i < initialSize; i++)
            {
                T obj = CreateNewObject();
                _objectPool.Enqueue(obj);
            }
        }

        /// <summary>
        /// Fetches an object from the pool. If the pool is empty, a new object is created.
        /// </summary>
        /// <returns>The pooled object.</returns>
        public T GetObject()
        {
            if (_objectPool.Count > 0)
            {
                T obj = _objectPool.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                return CreateNewObject();
            }
        }

        /// <summary>
        /// Returns an object to the pool and resets its state.
        /// </summary>
        /// <param name="obj">The object to return to the pool.</param>
        public void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.GetComponent<PooledObject>().ResetObject();
            _objectPool.Enqueue(obj);
        }

        /// <summary>
        /// Creates a new object from the prefab and returns it.
        /// </summary>
        /// <returns>The newly created object.</returns>
        private T CreateNewObject()
        {
            T newObject = Object.Instantiate(_prefab, _parent);
            newObject.gameObject.SetActive(false);
            return newObject;
        }
    }
}