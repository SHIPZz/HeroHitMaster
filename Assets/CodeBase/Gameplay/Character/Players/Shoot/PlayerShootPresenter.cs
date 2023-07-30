using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class PlayerShootPresenter : IInitializable, IDisposable
    {
        private const float ShootDelay = 0.3f;
        
        private readonly PlayerInput _playerInput;
        private readonly PlayerShoot _playerShoot;
        private readonly PlayerAnimator _playerAnimator;
        private bool _canShoot = true;

        public PlayerShootPresenter(PlayerInput playerInput, PlayerShoot playerShoot, PlayerAnimator playerAnimator)
        {
            _playerInput = playerInput;
            _playerShoot = playerShoot;
            _playerAnimator = playerAnimator;
        }

        public void Initialize() =>
            _playerInput.Fired += Shoot;

        public void Dispose() =>
            _playerInput.Fired -= Shoot;

        private void Shoot(Vector2 mousePosition)
        {
            if (_canShoot == false)
                return;

            _canShoot = false;

            _playerShoot.SetMousePosition(mousePosition);
            _playerAnimator.SetShooting(true);

            DOTween.Sequence().AppendInterval(ShootDelay).OnComplete(() =>
            {
                _canShoot = true;
                _playerAnimator.SetShooting(false);
            });
        }
    }
}