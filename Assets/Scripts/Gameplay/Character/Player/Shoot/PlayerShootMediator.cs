using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player.Shoot
{
    public class PlayerShootMediator : IInitializable, IDisposable
    {
        private readonly PlayerInput _playerInput;
        private readonly PlayerShoot _playerShoot;

        public PlayerShootMediator(PlayerInput playerInput, PlayerShoot playerShoot)
        {
            _playerInput = playerInput;
            _playerShoot = playerShoot;
        }

        public void Initialize() => 
            _playerInput.Fired += OnFiredClicked;

        public void Dispose() => 
            _playerInput.Fired -= OnFiredClicked;

        private void OnFiredClicked(Vector2 mousePosition) => 
            _playerShoot.Fire(mousePosition);
    }
}