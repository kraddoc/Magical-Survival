using UnityEngine;

namespace Project.Bullets
{
    public class Bullet : MonoBehaviour
    {
        private BulletPool _originPool;
        public void SetOriginPool(BulletPool pool) => _originPool = pool;
        public void ReturnToPool() => _originPool.ReturnBullet(this);
        
    }
}