using System;
using Constants;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyDestroyOnDeath : IInitializable, IDisposable
    {
        private readonly IHealth _enemyHealth;
        private float _delay;

        public EnemyDestroyOnDeath(IHealth enemyHealth)
        {
            _enemyHealth = enemyHealth;
            _delay = DelayValues.DefaultDestroyDelay;
        }

        public void Delay(float targetDelay) =>
            _delay = targetDelay;

        public void Initialize() => 
            _enemyHealth.ValueZeroReached += Destroy;

        public void Dispose() => 
            _enemyHealth.ValueZeroReached -= Destroy;
        
        private void Destroy() => 
            Object.Destroy(_enemyHealth.GameObject, _delay);
    }
}