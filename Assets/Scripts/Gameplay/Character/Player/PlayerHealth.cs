using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _healthValue;

        private IHealth _health;

        public IHealth Health => _health ??= new Health(_healthValue);

        public void TakeDamage(int value)
        {
            throw new System.NotImplementedException();
        }
    }
}