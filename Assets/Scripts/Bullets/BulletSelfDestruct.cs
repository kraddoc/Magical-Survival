using System;
using UnityEngine;

namespace Project.Bullets
{
    [RequireComponent(typeof(Bullet))]
    public class BulletSelfDestruct : MonoBehaviour
    {
        [SerializeField] private float maxLifetime = 4.0f;
        private Bullet _bullet;

        private void Start()
        {
            TryGetComponent(out _bullet);
        }

        private void OnEnable()
        {
            Invoke(nameof(SelfDestruct), maxLifetime);
        }

        private void SelfDestruct()
        {
            _bullet.ReturnToPool();
        }
    }
} 