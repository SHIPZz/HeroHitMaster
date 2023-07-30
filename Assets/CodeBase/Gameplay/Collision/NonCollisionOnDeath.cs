using System;
using CodeBase.Gameplay.Character;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Collision
{
    public class NonCollisionOnDeath : IInitializable, IDisposable
    {
        private readonly Collider _collider;
        private readonly IHealth _health;

        public NonCollisionOnDeath(Collider collider, IHealth health)
        {
            _health = health;
            _collider = collider;
        }

        public void Initialize() => 
            _health.ValueZeroReached += Disable;

        public void Dispose() => 
            _health.ValueZeroReached -= Disable;

        private void Disable() => 
            _collider.enabled = false;
    }
}