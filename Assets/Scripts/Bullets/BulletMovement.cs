using System;
using UnityEngine;

namespace Project.Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 500f;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            TryGetComponent(out _rigidbody);
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = transform.right * (_speed * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Destroy(gameObject);
        }
    }
}