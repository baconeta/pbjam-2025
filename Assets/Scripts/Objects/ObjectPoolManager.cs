namespace Objects
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Manages multiple object pools for different types of objects.
    /// Allows you to get a pool for different object types (e.g., bullets, enemies, etc.).
    /// </summary>
    public class ObjectPoolManager : MonoBehaviour
    {
        private readonly Dictionary<string, object> _pools = new Dictionary<string, object>();

        /// <summary>
        /// Creates or retrieves a pool for a specific type of object.
        /// </summary>
        /// <typeparam name="T">The type of object to pool. Must be a subclass of MonoBehaviour.</typeparam>
        /// <param name="prefab">The prefab of the object to pool.</param>
        /// <param name="initialSize">The initial number of objects to instantiate in the pool.</param>
        /// <returns>The pool for the specified object type.</returns>
        public ObjectPool<T> GetObjectPool<T>(T prefab, int initialSize = 10) where T : MonoBehaviour
        {
            string poolKey = prefab.name;

            if (!_pools.ContainsKey(poolKey))
            {
                ObjectPool<T> newPool = new ObjectPool<T>(prefab, initialSize, transform);
                _pools.Add(poolKey, newPool);
            }

            return (ObjectPool<T>)_pools[poolKey];
        }
    }
}