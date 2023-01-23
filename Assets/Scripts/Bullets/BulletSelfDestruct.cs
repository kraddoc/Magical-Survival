using System;
using UnityEngine;

namespace Project.Bullets
{
    [RequireComponent(typeof(Bullet))]
    public class BulletSelfDestruct : MonoBehaviour
    {
        [SerializeField] private float maxLifetime = 2.0f;
        private Bullet _bullet;
        private float _lifetime = 0f;

        private void Start()
        {
            TryGetComponent(out _bullet);
        }

        private void OnEnable()
        {
            _lifetime = 0;
        }

        private void Update()
        {
            _lifetime += Time.deltaTime;
            if (_lifetime >= maxLifetime)
                SelfDestruct();
        }

        private void SelfDestruct()
        {
            _bullet.ReturnToPool();
        }
    }
} 