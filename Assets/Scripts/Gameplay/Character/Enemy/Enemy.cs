using System;
using Enums;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public EnemyTypeId EnemyTypeId { get; private set; }
        [SerializeField] private int _healthValue;
        
        private IHealth _health;

        public event Action<int> Damaged;
        public event Action Dead;

        private void OnEnable()
        {
            _health = new Health(_healthValue);
            _health.ValueZeroReached += Die;
        }

        private void OnDisable()
        {
            _health.ValueZeroReached -= Die;
        }

        public void TakeDamage(int value)
        {
            _health.Decrease(value);
            Damaged?.Invoke(_health.CurrentValue);
        }

        private void Die() =>
            Dead?.Invoke();
    }
}