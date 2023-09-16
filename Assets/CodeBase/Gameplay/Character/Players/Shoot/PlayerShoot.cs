using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Providers;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class PlayerShoot
    {
        private readonly IProvider<CameraData> _cameraProvider;
        private readonly Transform _startShootPosition;
        private readonly IProvider<Weapon> _weaponProvider;

        private Weapon _weapon;
        private Vector2 _mousePosition;

        public PlayerShoot(IProvider<CameraData> cameraProvider,
            Transform startShootPosition, IProvider<Weapon> weaponProvider)
        {
            _cameraProvider = cameraProvider;
            _startShootPosition = startShootPosition;
            _weaponProvider = weaponProvider;
        }

        public void SetMousePosition(Vector2 mousePosition) => 
            _mousePosition = mousePosition;

        public void Fire()
        {
            Weapon weapon = _weaponProvider.Get();
            
            if (weapon is null)
                return;

            Ray ray = _cameraProvider.Get().Camera.ScreenPointToRay(_mousePosition);

            Vector3 targetVector;
            
            targetVector = !Physics.Raycast(ray, out RaycastHit hit) ? ray.GetPoint(70f) : hit.point;

            weapon.Shoot(targetVector, _startShootPosition.position);
        }
    }
}