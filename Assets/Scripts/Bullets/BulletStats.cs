using UnityEngine;

namespace Project.Bullets
{
    [CreateAssetMenu(fileName = "Bullet Stat Object", menuName = "Scriptable Objects/Stats/Bullet", order = 0)]
    public class BulletStats : ScriptableObject
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private float speed = 500;

        public int GetDamage => damage;
        public float GetSpeed => speed;
    }
}