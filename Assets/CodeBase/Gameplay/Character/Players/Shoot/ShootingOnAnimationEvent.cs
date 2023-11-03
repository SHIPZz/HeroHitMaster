using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class ShootingOnAnimationEvent : MonoBehaviour
    {
        private PlayerShoot _playerShoot;

        public event Action Shot;
        public event Action Stopped;

        [Inject]
        private void Construct(PlayerShoot playerShoot) =>
            _playerShoot = playerShoot;

        public void OnAnimationShoot()
        {
            _playerShoot.Fire();
            Shot?.Invoke();
        }

        public void OnAnimationStoppedShoot() =>
            Stopped?.Invoke();
    }
}