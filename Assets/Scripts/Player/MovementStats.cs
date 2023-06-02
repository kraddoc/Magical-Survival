using UnityEngine;

namespace Project.Player
{
    [CreateAssetMenu(fileName = "Player Movement Stats", menuName = "Scriptable Objects/Stats/PlayerMovement", order = 0)]
    public class MovementStats : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float speed = 100f;
        [SerializeField] private float accelerationFactor = 5f;
        [SerializeField] private float decelerationFactor = 10f;
        [SerializeField] private float directionSmoothFactor = 0.02f;
        [Header("Dash")] 
        [SerializeField] private float dashSpeed = 500f;
        [SerializeField] private float dashTotalTime = 0.2f;

        public float Speed => speed;

        public float AccelerationFactor => accelerationFactor;

        public float DecelerationFactor => decelerationFactor;

        public float DirectionSmoothFactor => directionSmoothFactor;

        public float DashSpeed => dashSpeed;

        public float DashTotalTime => dashTotalTime;
    }
}