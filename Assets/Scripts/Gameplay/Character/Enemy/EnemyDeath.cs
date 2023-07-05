using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Gameplay.Character.Enemy
{
    public class EnemyDeath : IInitializable, IDisposable
    {
        private const float DestroyDelay = 3f;
        
        private readonly Enemy _enemy;
        private readonly Collider _collider;

        public EnemyDeath(Enemy enemy, Collider collider)
        {
            _enemy = enemy;
            _collider = collider;
        }
        
        public void Initialize()
        {
            _enemy.Dead += Disable;
        }

        public void Dispose()
        {
            _enemy.Dead -= Disable;
        }

        private void Disable()
        {
            _collider.enabled = false;
            // Object.Destroy(_enemy.gameObject, DestroyDelay);
        }
    }
}