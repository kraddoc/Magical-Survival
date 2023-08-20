using System.Collections;
using Project.Bullets;
using Project.Player;
using UnityEngine;

namespace Project.Weapons
{
    public class WeaponFire : MonoBehaviour
    {
        [SerializeField] private InputHandler input;
        [SerializeField] private BulletPool pool;
        [SerializeField] private GameObject muzzle;

        [Header("Wand stats.")] [SerializeField] private float shotCooldown = 0.2f;
        private bool _canShoot = true;
        
        private void Update()
        {
            if (!input.FireKey || !_canShoot) return;
            
            Fire();
            StartCoroutine(Cooldown());
        }

        private void Fire()
        {
            var bullet = pool.GetBullet().transform;
            bullet.rotation = muzzle.transform.rotation;
            bullet.position = muzzle.transform.position;
        }

        private IEnumerator Cooldown()
        {
            _canShoot = false;

            yield return new WaitForSeconds(shotCooldown);

            _canShoot = true;
        }
    }
}