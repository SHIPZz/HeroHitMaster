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
        public event Action ValueRecovered;
        
        public void Decrease(int value)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - value, 0, MaxValue);
            
            if(CurrentValue == 0)
                ValueZeroReached?.Invoke();
            
            ValueChanged?.Invoke(CurrentValue);
        }

        public void Heal(int value)
        {
            CurrentValue = Mathf.Clamp(CurrentValue + value, 0, MaxValue);
            
            ValueChanged?.Invoke(value);
        }
    }
}