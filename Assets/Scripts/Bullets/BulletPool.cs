using System;
using UnityEngine;

namespace Project.Bullets
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int poolSize;
        private ObjectPool _pool;
        
        private void Start()
        {
            _pool = new ObjectPool(bulletPrefab, poolSize);
        }

        public void ActivateBullet()
        {
            _pool.GetFromPool();
        }

        public void ReturnBullet(GameObject bullet)
        {
            _pool.ReturnToPool(bullet);
            
        }
    }
}