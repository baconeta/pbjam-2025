using UnityEngine;

namespace Objects
{
    public class ExampleBulletSpawner : MonoBehaviour
    {
        [SerializeField] private ExampleBullet bulletPrefab;

        private ObjectPool<ExampleBullet> _bulletPool;

        private void Start()
        {
            // Create the pool for bullets (size of 10 for example)
            _bulletPool = FindAnyObjectByType<ObjectPoolManager>().GetObjectPool<ExampleBullet>(bulletPrefab.gameObject, 10);
        }

        public void SpawnBullet(Vector3 position, Vector3 direction)
        {
            var bullet = _bulletPool.Get();
            bullet.transform.position = position;
            bullet.gameObject.SetActive(true);

            // Do stuff with the bullet
        }

        public void ReturnBulletToPool(ExampleBullet bullet)
        {
            _bulletPool.Return(bullet);
        }
    }
}