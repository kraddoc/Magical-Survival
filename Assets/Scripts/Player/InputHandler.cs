using System;
using UnityEngine;

namespace Project.Player
{
    public class InputHandler : MonoBehaviour
    {
        public event Action DashKeyPressed;

        public bool FireKey => Input.GetMouseButton(0);
        
        public Vector2 InputDirection => _inputDirectionRaw.normalized;
        public bool NoMovementInput => InputDirection == Vector2.zero;
        public Vector2 LastValidDirection { get; private set; } = Vector2.right;
        
        
        private Vector2 _inputDirectionRaw;

        //TODO maybe add some way to keep diagonal movement as last valid direction

        private void Update()
        {
            _inputDirectionRaw.x = Input.GetAxisRaw("Horizontal");
            _inputDirectionRaw.y = Input.GetAxisRaw("Vertical");

            if (NoMovementInput) return;
            LastValidDirection = InputDirection;
            
            if (Input.GetKeyDown(KeyCode.Space))
                DashKeyPressed?.Invoke();

        }
    }
}
