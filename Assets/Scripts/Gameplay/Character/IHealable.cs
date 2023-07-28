using System;

namespace Gameplay.Character
{
    public interface IHealable
    {
        public event Action<int> Recovered; 
        void Heal(int value);
    }
}