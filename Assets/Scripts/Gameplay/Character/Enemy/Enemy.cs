using UnityEngine;

namespace Gameplay.Character.Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public void TakeDamage(int value)
        {
            Destroy(gameObject);
        }
    }
}