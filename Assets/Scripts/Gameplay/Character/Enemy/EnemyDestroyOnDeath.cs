using System;
using Constants;
using Zenject;
using Object = UnityEngine.Object;

namespace Gameplay.Character.Enemy
{
    public class EnemyDestroyOnDeath : IInitializable, IDisposable
    {
        private readonly EnemyHealth _enemyHealth;
        private float _delay;

        public EnemyDestroyOnDeath(EnemyHealth enemyHealth)
        {
            _enemyHealth = enemyHealth;
            _delay = DelayValues.DefaultDestroyDelay;
        }

        public void Delay(float targetDelay) =>
            _delay = targetDelay;

        public void Initialize() => 
            _enemyHealth.Dead += Destroy;

        public void Dispose()
        {
            _enemyHealth.Dead -= Destroy;
        }

        private void Destroy() => 
            Object.Destroy(_enemyHealth.gameObject, _delay);
    }
}