namespace Objects
{
    using UnityEngine;

    /// <summary>
    /// This class is attached to any object that will be pooled.
    /// It is responsible for resetting the object's state when it is returned to the pool.
    /// </summary>
    public class PooledObject : MonoBehaviour
    {
        /// <summary>
        /// Called to reset the object's state when it is returned to the pool.
        /// Override this method to reset specific properties (like position, velocity, etc.).
        /// </summary>
        public virtual void ResetObject()
        {
            // Default reset behavior: disable the object and reset any variables
            gameObject.SetActive(false);
        }
    }
}