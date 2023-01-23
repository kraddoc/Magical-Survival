using System;
using UnityEngine;

namespace Project.Bullets
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private int poolSize = 50;
        private ObjectPool _pool;
        
        private void Start()
        {
            _pool = new ObjectPool(bulletPrefab, poolSize, transform);
        }

        public Bullet GetBullet()
        {
            var bullet = (Bullet)_pool.GetFromPool();
            bullet.SetOriginPool(this);
            return bullet;
        }

        public void ReturnBullet(Bullet bullet)
        {
            _pool.ReturnToPool(bullet);
        }
    }
}