using DG.Tweening;
using Services;
using Services.Providers;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player.Shoot
{
    public class ShootInputMediator : ITickable
    {
        private const float ShootDistance = 100;
        private const float ShootDelay = 0.3f;
        private readonly IInputService _inputService;
        private readonly CameraProvider _cameraProvider;
        private readonly Transform _righthand;
        private readonly WeaponsProvider _weaponsProvider;
        private bool _canShoot = true;
        private Weapon.Weapon _weapon;

        public ShootInputMediator(IInputService inputService, CameraProvider cameraProvider,
            Transform righthand, WeaponsProvider weaponsProvider)
        {
            _inputService = inputService;
            _cameraProvider = cameraProvider;
            _righthand = righthand;
            _weaponsProvider = weaponsProvider;
        }

        public void Tick()
        {
            Weapon.Weapon weapon = _weaponsProvider.CurrentWeapon;
            
            if (!_inputService.PlayerFire.WasPressedThisFrame() || !_canShoot || weapon is null )
                return;

            Vector2 mousePosition = _inputService.MousePosition;
            Ray ray = _cameraProvider.Camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                weapon.Shoot(hit.point, _righthand.position);
            }
            else
            {
                Vector3 target = ray.GetPoint(ShootDistance);
                weapon.Shoot(target, _righthand.position);
            }

            _canShoot = false;

            DOTween.Sequence().AppendInterval(ShootDelay).OnComplete(() => _canShoot = true);
        }
    }
}