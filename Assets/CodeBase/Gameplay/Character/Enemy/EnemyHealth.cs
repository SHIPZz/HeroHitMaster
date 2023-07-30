using System;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth, IDamageable
    {
        [field: SerializeField] public int CurrentValue { get; private set; }

        [field: SerializeField] public int MaxValue { get; private set; }
        public GameObject GameObject => gameObject;

        public event Action<int> ValueChanged;
        public event Action ValueZeroReached;

        public void TakeDamage(int value)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - value, 0, MaxValue);

            if (CurrentValue == 0)
                ValueZeroReached?.Invoke();

            ValueChanged?.Invoke(CurrentValue);
        }
    }
}