using System;
using UnityEngine;

namespace Gameplay.Character
{
    public interface IHealth
    {
        GameObject GameObject { get; }
        int CurrentValue { get; }
        int MaxValue { get; }
        
        event Action<int> ValueChanged;
        event Action ValueZeroReached;
    }
}