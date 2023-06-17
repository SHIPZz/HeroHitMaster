using System;
using UnityEngine;

namespace Gameplay.Character
{
    public class Health : IHealth
    {
        public Health(int value)
        {
            CurrentValue = value;
            MaxValue = CurrentValue;
        }
        
        public int CurrentValue { get; private set; }
        public int MaxValue { get; private set; }
        public event Action<int> ValueChanged;
        public event Action ValueZeroReached;
        
        public void TakeDamage(int value)
        {
            Clamp(-value);
            
            if(CurrentValue == 0)
                ValueZeroReached?.Invoke();
            
            ValueChanged?.Invoke(CurrentValue);
        }

        public void Heal(int value)
        {
            Clamp(value);
            ValueChanged?.Invoke(value);
        }

        private void Clamp(int value) => 
            CurrentValue = Mathf.Clamp(CurrentValue + value, 0, MaxValue);
    }
}