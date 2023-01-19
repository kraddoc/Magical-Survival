﻿using System;
using UnityEngine;

namespace Project.Wand
{
    public class WandToMouseRotation : MonoBehaviour
    {
        [SerializeField] private float _smoothTime = 0.1f;
        private Camera _camera;
        private Vector2 _mousePosition;
        private Transform _transform;
        private Vector2 _direction = Vector2.right;
        private Vector2 _velocity;

        private void Start()
        {
            _camera = Camera.main;
            TryGetComponent(out _transform);
        }

        private Vector2 MousePosition => _camera.ScreenToWorldPoint(Input.mousePosition);

        private void Update()
        {
            _direction = Vector2.SmoothDamp(_direction, (MousePosition - (Vector2) _transform.position).normalized, ref _velocity, _smoothTime);
            _transform.right = _direction;
        }
        
    }
}