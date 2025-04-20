using UnityEngine;

namespace Objects
{
    public class ExampleBullet : PooledObject
    {
        // Bullet-specific logic here, such as movement, collision, etc.

        public override void ResetObject()
        {
            base.ResetObject();
            // Reset bullet-specific properties like velocity, position, etc.
        }
    }

    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private ExampleBullet bulletPrefab;

        private ObjectPool<ExampleBullet> _bulletPool;

        private void Start()
        {
            // Create the pool for bullets (size of 10 for example)
            _bulletPool = FindAnyObjectByType<ObjectPoolManager>().GetObjectPool(bulletPrefab, 10);
        }

        public void SpawnBullet(Vector3 position, Vector3 direction)
        {
            var bullet = _bulletPool.GetObject();
            bullet.transform.position = position;
            bullet.gameObject.SetActive(true);

            // Do stuff with the bullet
        }

        public void ReturnBulletToPool(ExampleBullet bullet)
        {
            _bulletPool.ReturnObject(bullet);
        }
    }
}