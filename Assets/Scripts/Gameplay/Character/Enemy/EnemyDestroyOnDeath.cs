using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Gameplay.Character.Enemy
{
    public class EnemyDestroyOnDeath : IInitializable, IDisposable
    {
        private readonly EnemyHealth _enemyHealth;
        private readonly Collider _collider;
        private float _delay = 0.1f;

        public EnemyDestroyOnDeath(EnemyHealth enemyHealth, Collider collider)
        {
            _enemyHealth = enemyHealth;
            _collider = collider;
        }

        public void Delay(float targetDelay) =>
            _delay = targetDelay;

        public void Initialize() => 
            _enemyHealth.Dead += Destroy;

        public void Dispose() => 
            _enemyHealth.Dead -= Destroy;

        private void Destroy()
        {
            _collider.enabled = false;
            Object.Destroy(_enemyHealth.gameObject, _delay);
        }
    }
}