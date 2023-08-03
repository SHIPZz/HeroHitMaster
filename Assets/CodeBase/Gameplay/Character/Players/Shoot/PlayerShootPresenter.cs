using System;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class PlayerShootPresenter : IInitializable, IDisposable
    {
        
        private float _shootDelay;
        private readonly PlayerInput _playerInput;
        private readonly PlayerShoot _playerShoot;
        private readonly PlayerAnimator _playerAnimator;
        private readonly WeaponProvider _weaponProvider;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private bool _canShoot = true;

        public PlayerShootPresenter(PlayerInput playerInput, PlayerShoot playerShoot, PlayerAnimator playerAnimator, 
            WeaponProvider weaponProvider, WeaponStaticDataService weaponStaticDataService)
        {
            _playerInput = playerInput;
            _playerShoot = playerShoot;
            _playerAnimator = playerAnimator;
            _weaponProvider = weaponProvider;
            _weaponStaticDataService = weaponStaticDataService;
        }

        public void Initialize() =>
            _playerInput.Fired += Shoot;

        public void Dispose() =>
            _playerInput.Fired -= Shoot;

        private void Shoot(Vector2 mousePosition)
        {
            _shootDelay = _weaponStaticDataService.Get(_weaponProvider.CurrentWeapon.WeaponTypeId).ShootDelay;
            
            if (_canShoot == false)
                return;

            _canShoot = false;

            _playerShoot.SetMousePosition(mousePosition);
            _playerAnimator.SetShooting(true);

            DOTween.Sequence().AppendInterval(_shootDelay).OnComplete(() =>
            {
                _canShoot = true;
                _playerAnimator.SetShooting(false);
            });
        }
    }
}