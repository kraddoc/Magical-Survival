using UnityEngine;

namespace Project.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletStats stats;
        private BulletPool _originPool;

        public float BaseSpeed => stats.GetSpeed;
        
        public void SetOriginPool(BulletPool pool) => _originPool = pool;
        public void ReturnToPool() => _originPool.ReturnBullet(this);
        
    }
}