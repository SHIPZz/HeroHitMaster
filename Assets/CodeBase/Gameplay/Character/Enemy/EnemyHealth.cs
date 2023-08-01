using System;
using CodeBase.Enums;
using CodeBase.Services.Storages;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth, IDamageable
    {
        public int CurrentValue { get; private set; }
        public int MaxValue { get; private set; }
        public GameObject GameObject => gameObject;

        public event Action<int> ValueChanged;
        public event Action ValueZeroReached;

        [Inject]
        private void Construct(EnemyStaticDataService enemyStaticDataService, EnemyTypeId enemyTypeId)
        {
            CurrentValue = enemyStaticDataService.GetEnemyData(enemyTypeId).Hp;
            print(CurrentValue);
            MaxValue = CurrentValue;
        }

        public void TakeDamage(int value)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - value, 0, MaxValue);

            if (CurrentValue == 0)
                ValueZeroReached?.Invoke();

            ValueChanged?.Invoke(CurrentValue);
        }
    }
}