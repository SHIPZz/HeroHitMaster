using System;

namespace CodeBase.Gameplay.Character
{
    public interface IHealable
    {
        public event Action<int> Recovered; 
        void Heal(int value);
    }
}