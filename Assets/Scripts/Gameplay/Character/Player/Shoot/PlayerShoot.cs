using DG.Tweening;
using Gameplay.Bullet;
using Services.Providers;
using UnityEngine;

namespace Gameplay.Character.Player.Shoot
{
    public class PlayerShoot
    {
        private const float ShootDistance = 100;
        private const float ShootDelay = 0.5f;

        private readonly CameraProvider _cameraProvider;
        private readonly Transform _startShootPosition;
        private readonly WeaponsProvider _weaponsProvider;
        private readonly PlayerAnimator _playerAnimator;

        private bool _canShoot = true;
        private Weapon.Weapon _weapon;

        public PlayerShoot(CameraProvider cameraProvider,
            Transform startShootPosition, WeaponsProvider weaponsProvider, PlayerAnimator playerAnimator)
        {
            _cameraProvider = cameraProvider;
            _startShootPosition = startShootPosition;
            _weaponsProvider = weaponsProvider;
            _playerAnimator = playerAnimator;
        }

        public void Fire(Vector2 mousePosition)
        {
            Weapon.Weapon weapon = _weaponsProvider.CurrentWeapon;

            if (!_canShoot || weapon is null)
                return;

            Ray ray = _cameraProvider.Camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                weapon.Shoot(hit.point, _startShootPosition.position);
                _playerAnimator.SetShooting(true);
            }
            else
            {
                Vector3 target = ray.GetPoint(ShootDistance);
                weapon.Shoot(target, _startShootPosition.position);
                _playerAnimator.SetShooting(true);
            }

            _canShoot = false;

            DOTween.Sequence().AppendInterval(ShootDelay).OnComplete(() =>
            {
                _canShoot = true;
                _playerAnimator.SetShooting(false);
            });
        }
    }
}