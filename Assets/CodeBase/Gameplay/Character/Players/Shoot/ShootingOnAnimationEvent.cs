using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Players.Shoot
{
    public class ShootingOnAnimationEvent : MonoBehaviour
    {
        private PlayerShoot _playerShoot;

        public event Action Shooted;
        public event Action Stopped;

        [Inject]
        private void Construct(PlayerShoot playerShoot) =>
            _playerShoot = playerShoot;

        public void OnAnimationShoot()
        {
            _playerShoot.Fire();
            Shooted?.Invoke();
        }

        public void OnAnimationStoppedShoot() =>
            Stopped?.Invoke();
    }
}