using UnityEngine;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Inspector Fields

        [Header("References")]
        [SerializeField] private InputHandler input;
        [Header("Movement")]
        [SerializeField] private float speed = 100f;
        [SerializeField] private float accelerationFactor = 5f;
        [SerializeField] private float decelerationFactor = 10f;
        [SerializeField] private float directionSmoothFactor = 0.02f;
        [Header("Dash")] 
        [SerializeField] private float dashSpeed = 500f;
        [SerializeField] private float dashTotalTime = 0.2f;

        #endregion

        #region private values

        //movement values
        private Rigidbody2D _rigidbody2D;
        private float _currentSpeed = 0f;
        private float _accelerationTimer = 0f;
        private Vector2 _movementDirection = Vector2.zero;
        
        //direction smoothing stuff
        private bool AlmostStopped => _currentSpeed / speed > StopPercentage;
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
            input.DashKeyPressed += DashInputCaught;
        }

        private void OnDisable()
        {
            input.DashKeyPressed -= DashInputCaught;
        }


        #endregion
        
        private void Update()
        {
            //DASH CODE
            if (_isDashing)
            {
                _dashTimer += Time.deltaTime;
                _movementDirection = _dashDirection;
                _currentSpeed = dashSpeed;
                if (_dashTimer >= dashTotalTime) _isDashing = false;
                else return;
            }
            //DASH CODE END
            
            _accelerationTimer = Mathf.Clamp01(_accelerationTimer);

            if (input.NoMovementInput)
                _accelerationTimer -= Time.deltaTime * decelerationFactor;
            else
                _accelerationTimer += Time.deltaTime * accelerationFactor;

            _currentSpeed = Mathf.Lerp(0, speed, _accelerationTimer);

            //if almost stopped movement no need to smooth direction
            _movementDirection = AlmostStopped ? 
                SmoothDirection(input.LastValidDirection, _movementDirection).normalized :
                input.LastValidDirection;
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
                Vector2.SmoothDamp(currentDirection, inputDirection, ref _directionSmoothingVelocity, directionSmoothFactor);
        }

        private void DashInputCaught()
        {
            if (_isDashing) return;
            
            _isDashing = true;
            _dashDirection = input.LastValidDirection;
            _dashTimer = 0f;
            _accelerationTimer = 1f;
        }
    }
}
