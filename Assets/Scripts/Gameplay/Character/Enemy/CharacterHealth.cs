using System;
using Enums;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class CharacterHealth : MonoBehaviour, IDamageable, IInitializable, IDisposable
    {
        [SerializeField] private int _healthValue;

        private IHealth _health;

        public IHealth Health => _health;

        public event Action<int> Damaged;
        public event Action Dead;

        public void TakeDamage(int value)
        {
            print(gameObject.name);
            _health.Decrease(value);
            Damaged?.Invoke(_health.CurrentValue);
        }

        private void Die() =>
            Dead?.Invoke();

        public void Initialize()
        {
            _health = new Health(_healthValue);
            _health.ValueZeroReached += Die;
        }

        public void Dispose() => 
            _health.ValueZeroReached -= Die;
    }
}