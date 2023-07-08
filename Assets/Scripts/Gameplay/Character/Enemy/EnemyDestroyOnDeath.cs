using System;
using Constants;
using Zenject;
using Object = UnityEngine.Object;

namespace Gameplay.Character.Enemy
{
    public class EnemyDestroyOnDeath : IInitializable, IDisposable
    {
        private readonly CharacterHealth _characterHealth;
        private float _delay;

        public EnemyDestroyOnDeath(CharacterHealth characterHealth)
        {
            _characterHealth = characterHealth;
            _delay = DelayValues.DefaultDestroyDelay;
        }

        public void Delay(float targetDelay) =>
            _delay = targetDelay;

        public void Initialize() => 
            _characterHealth.Dead += Destroy;

        public void Dispose()
        {
            _characterHealth.Dead -= Destroy;
        }

        private void Destroy()
        {
            // Object.Destroy(characterHealth.gameObject, _delay);
        }
    }
}