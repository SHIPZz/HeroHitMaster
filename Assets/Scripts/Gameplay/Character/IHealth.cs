using System;

namespace Gameplay.Character
{
    public interface IHealth
    {
        int CurrentValue { get; }
        int MaxValue { get; }
        
        event Action<int> ValueChanged;
        event Action ValueZeroReached;

        void Heal(int value);
        void Decrease(int value);
    }
}