using System;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class PlayerShootPresenter : IInitializable, IDisposable
    {
        private readonly PlayerShootInput _playerShootInput;
        private readonly PlayerShoot _playerShoot;
        private readonly PlayerAnimator _playerAnimator;
        private readonly IProvider<Weapon> _weaponProvider;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private bool _canShoot = true;
        private float _shootDelay;

        public PlayerShootPresenter(PlayerShootInput playerShootInput, PlayerShoot playerShoot,
            PlayerAnimator playerAnimator,
            IProvider<Weapon> weaponProvider, WeaponStaticDataService weaponStaticDataService)
        {
            _playerShootInput = playerShootInput;
            _playerShoot = playerShoot;
            _playerAnimator = playerAnimator;
            _weaponProvider = weaponProvider;
            _weaponStaticDataService = weaponStaticDataService;
        }

        public void Initialize() =>
            _playerShootInput.Fired += Shoot;

        public void Dispose() =>
            _playerShootInput.Fired -= Shoot;

        private void Shoot(Vector2 mousePosition)
        {
            _shootDelay = _weaponStaticDataService.Get(_weaponProvider.Get().WeaponTypeId).ShootDelay;

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