using System;
using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Gameplay.Character
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