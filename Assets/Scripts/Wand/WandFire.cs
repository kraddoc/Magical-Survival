using System;
using Project.Bullets;
using Project.Player;
using UnityEngine;

namespace Project.Wand
{
    public class WandFire : MonoBehaviour
    {
        [SerializeField] private InputHandler input;
        [SerializeField] private BulletPool pool;
        [SerializeField] private GameObject muzzle;

        [Header("Wand stats.")] [SerializeField] private float shotCooldown = 0.2f;
        private float _currentCooldown = 0f;
        private bool CanShoot => shotCooldown <= _currentCooldown;
        
        private void Update()
        {
            _currentCooldown += Time.deltaTime;
            _currentCooldown = Mathf.Clamp(_currentCooldown, 0, shotCooldown);
            
            if (input.FireKey && CanShoot)
            {
                Fire();
                _currentCooldown -= shotCooldown;
            }
        }

        private void Fire()
        {
            var bullet = pool.GetBullet();
            bullet.transform.rotation = muzzle.transform.rotation;
            bullet.transform.position = muzzle.transform.position;
        }
    }
}