using System;
using DG.Tweening;
using Enums;
using Services;
using Services.Factories;
using Services.Providers;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player.Shoot
{
    public class ShootInputMediator : ITickable, IInitializable, IDisposable
    {
        private const float ShootDistance = 100;
        private const float ShootDelay = 0.3f;
        private readonly IInputService _inputService;
        private readonly CameraProvider _cameraProvider;
        private readonly Transform _righthand;
        private readonly WeaponSelector _weaponSelector;
        private readonly GameFactory _gameFactory;
        private bool _canShoot = true;
        private Weapon.Weapon _weapon;

        public ShootInputMediator(IInputService inputService, CameraProvider cameraProvider,
            Transform righthand, WeaponSelector weaponSelector, GameFactory gameFactory)
        {
            _inputService = inputService;
            _cameraProvider = cameraProvider;
            _righthand = righthand;
            _weaponSelector = weaponSelector;
            _gameFactory = gameFactory;
        }

        public void Initialize()
        {
            _weaponSelector.WeaponSelected += SetWeapon;
        }

        public void Tick()
        {
            if (!_inputService.PlayerFire.WasPressedThisFrame() || !_canShoot || _weapon is null)
                return;

            Vector2 mousePosition = _inputService.MousePosition;
            Ray ray = _cameraProvider.Camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _weapon.Shoot(hit.point, _righthand.position);
            }
            else
            {
                Vector3 target = ray.GetPoint(ShootDistance);
                _weapon.Shoot(target, _righthand.position);
            }

            _canShoot = false;

            DOTween.Sequence().AppendInterval(ShootDelay).OnComplete(() => _canShoot = true);
        }

        public void Dispose()
        {
            _weaponSelector.WeaponSelected -= SetWeapon;
        }

        private void SetWeapon(WeaponTypeId weaponTypeId)
        {
            _weapon = _gameFactory.CreateWeapon(weaponTypeId);
        }
    }
}