using UnityEngine;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Inspector Fields

        [Header("References")]
        [SerializeField] private InputHandler _input;
        [Header("Movement")]
        [SerializeField] private float _speed = 100f;
        [SerializeField] private float _accelerationFactor = 5f;
        [SerializeField] private float _decelerationFactor = 10f;
        [SerializeField] private float _directionSmoothFactor = 0.02f;
        [Header("Dash")] 
        [SerializeField] private float _dashSpeed = 500f;
        [SerializeField] private float _dashTotalTime = 0.2f;

        #endregion

        #region private values

        //movement values
        private Rigidbody2D _rigidbody2D;
        private float _currentSpeed = 0f;
        private float _accelerationTimer = 0f;
        private Vector2 _movementDirection = Vector2.zero;
        
        //direction smoothing stuff
        private bool AlmostStopped => _currentSpeed / _speed > StopPercentage;
        private const float StopPercentage = 0.1f; //percentage of speed at which player is considered to stopped
        private Vector2 _directionSmoothingVelocity = Vector2.zero;
        
        //dashing values
        private bool _isDashing = false;
        private Vector2 _dashDirection = Vector2.right;
        private float _dashTimer = 0f;

        #endregion
        
        #region game start logic

        private void Start()
        {
            TryGetComponent(out _rigidbody2D);
        }

        private void OnEnable()
        {
            _input.DashKeyPressed += DashInputCaught;
        }

        private void OnDisable()
        {
            _input.DashKeyPressed -= DashInputCaught;
        }


        #endregion
        
        private void Update()
        {
            //DASH CODE
            if (_isDashing)
            {
                _dashTimer += Time.deltaTime;
                _movementDirection = _dashDirection;
                _currentSpeed = _dashSpeed;
                if (_dashTimer >= _dashTotalTime) _isDashing = false;
                else return;
            }
            //DASH CODE END
            
            _accelerationTimer = Mathf.Clamp01(_accelerationTimer);

            if (_input.NoMovementInput)
                _accelerationTimer -= Time.deltaTime * _decelerationFactor;
            else
                _accelerationTimer += Time.deltaTime * _accelerationFactor;

            _currentSpeed = Mathf.Lerp(0, _speed, _accelerationTimer);

            //if almost stopped movement no need to smooth direction
            _movementDirection = AlmostStopped ? 
                SmoothDirection(_input.LastValidDirection, _movementDirection).normalized :
                _input.LastValidDirection;
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = _movementDirection * (_currentSpeed * Time.deltaTime);
        }

        private Vector2 SmoothDirection(Vector2 inputDirection, Vector2 currentDirection)
        {   
            
            //if suddenly changed direction to opposite no need for smoothing
            return inputDirection + currentDirection == Vector2.zero ? 
                inputDirection : 
                Vector2.SmoothDamp(currentDirection, inputDirection, ref _directionSmoothingVelocity, _directionSmoothFactor);
        }

        private void DashInputCaught()
        {
            if (_isDashing) return;
            
            _isDashing = true;
            _dashDirection = _input.LastValidDirection;
            _dashTimer = 0f;
            _accelerationTimer = 1f;
        }
    }
}
