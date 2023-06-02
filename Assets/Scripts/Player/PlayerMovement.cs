using UnityEngine;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Inspector Fields

        [Header("References")]
        [SerializeField] private InputHandler input;
        [SerializeField] private MovementStats stats;

        #endregion

        #region private values

        //movement values
        private Rigidbody2D _rigidbody2D;
        private float _currentSpeed = 0f;
        private float _accelerationTimer = 0f;
        private Vector2 _movementDirection = Vector2.zero;
        
        //direction smoothing stuff
        private bool AlmostStopped => _currentSpeed / stats.Speed > StopPercentage;
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
                _currentSpeed = stats.DashSpeed;
                if (_dashTimer >= stats.DashTotalTime) _isDashing = false;
                else return;
            }
            //DASH CODE END
            
            _accelerationTimer = Mathf.Clamp01(_accelerationTimer);

            if (input.NoMovementInput)
                _accelerationTimer -= Time.deltaTime * stats.DecelerationFactor;
            else
                _accelerationTimer += Time.deltaTime * stats.AccelerationFactor;

            _currentSpeed = Mathf.Lerp(0, stats.Speed, _accelerationTimer);

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
                Vector2.SmoothDamp(currentDirection, inputDirection, ref _directionSmoothingVelocity, stats.DirectionSmoothFactor);
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
