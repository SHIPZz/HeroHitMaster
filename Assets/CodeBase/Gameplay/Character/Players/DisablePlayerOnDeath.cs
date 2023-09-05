using System;
using Zenject;

namespace CodeBase.Gameplay.Character.Players
{
    public class DisablePlayerOnDeath : IInitializable, IDisposable
    {
        private readonly IHealth _health;

        public DisablePlayerOnDeath(IHealth health) =>
            _health = health;

        public void Initialize() => 
            _health.ValueZeroReached += Disable;

        public void Dispose() => 
            _health.ValueZeroReached -= Disable;

        private void Disable() =>
            _health.GameObject.SetActive(false);
        
    }
}