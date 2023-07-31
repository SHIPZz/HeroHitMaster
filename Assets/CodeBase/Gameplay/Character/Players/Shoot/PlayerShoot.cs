using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Providers;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class PlayerShoot
    {
        private readonly CameraProvider _cameraProvider;
        private readonly Transform _startShootPosition;
        private readonly WeaponProvider _weaponProvider;

        private Weapon _weapon;
        private Vector2 _mousePosition;

        public PlayerShoot(CameraProvider cameraProvider,
            Transform startShootPosition, WeaponProvider weaponProvider)
        {
            _cameraProvider = cameraProvider;
            _startShootPosition = startShootPosition;
            _weaponProvider = weaponProvider;
        }

        public void SetMousePosition(Vector2 mousePosition)
        {
            _mousePosition = mousePosition;
        }

        public void Fire()
        {
            Weapon weapon = _weaponProvider.CurrentWeapon;
            
            if (weapon is null)
                return;

            Ray ray = _cameraProvider.Camera.ScreenPointToRay(_mousePosition);

            Vector3 targetVector;
            
            targetVector = !Physics.Raycast(ray, out RaycastHit hit) ? ray.GetPoint(70f) : hit.point;
            
            weapon.Shoot(targetVector, _startShootPosition.position);
        }
    }
}