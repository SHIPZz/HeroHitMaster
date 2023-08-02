using System;
using CodeBase.Enums;
using CodeBase.Services.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Players
{
    public class PlayerHealth : MonoBehaviour, IHealth, IHealable, IDamageable
    {
        public int CurrentValue { get; private set; }

        public int MaxValue { get; private set; }
        public GameObject GameObject => gameObject;

        public event Action<int> ValueChanged;

        public event Action ValueZeroReached;

        public event Action<int> Recovered;

        [Inject]
        private void Construct(PlayerStaticDataService playerStaticDataService, PlayerTypeId playerTypeId)
        {
            CurrentValue = playerStaticDataService.GetPlayerData(playerTypeId).Hp;
            MaxValue = CurrentValue;
        }

        public void TakeDamage(int value)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - value, 0, MaxValue);

            if (CurrentValue == 0)
                ValueZeroReached?.Invoke();

            ValueChanged?.Invoke(CurrentValue);
        }

        public void Heal(int value)
        {
            CurrentValue = Mathf.Clamp(CurrentValue + value, 0, MaxValue);

            Recovered?.Invoke(CurrentValue);
        }
    }
}