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

        private void Update()
        {
            if (input.FireKey) Fire();
        }

        private void Fire()
        {
            pool.ActivateBullet();
        }
    }
}