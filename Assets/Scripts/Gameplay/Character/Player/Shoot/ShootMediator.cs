using DG.Tweening;
using Gameplay.Web;
using Services;
using Services.Inputs.Weapon;
using Services.Providers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay.Character.Player.Shoot
{
    public class ShootMediator : ITickable
    {
        private readonly IInputService _inputService;
        private readonly CameraProvider _cameraProvider;
        private readonly Transform _righthand;
        private readonly GameObjectPoolProvider _gameObjectPoolProvider;
        private readonly WeaponSelectorHandler _weaponSelectorHandler;
        private IWeapon _weapon;
        private GameObject _webPrefab;
        private Web.Web _web;
        private bool _canShoot;

        //get weapon from weaponselectorhandler
        public ShootMediator(IInputService inputService, CameraProvider cameraProvider, 
            Transform righthand, 
            GameObjectPoolProvider gameObjectPoolProvider, 
            WeaponSelectorHandler weaponSelectorHandler)
        {
            _inputService = inputService;
            _cameraProvider = cameraProvider;
            _righthand = righthand;
            _canShoot = true;
            _weaponSelectorHandler = weaponSelectorHandler;
            _gameObjectPoolProvider = gameObjectPoolProvider;
        }

        public void Tick()
        {
            if (_inputService.PlayerFire.WasPressedThisFrame() && _canShoot)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                Ray ray = _cameraProvider.Camera.ScreenPointToRay(mousePosition);
                GameObject webGameObject = null;
                GameObject web = _gameObjectPoolProvider.GameObjectPool.Pop();

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    _weapon.Shoot(hit.point, _righthand.position,web.GetComponent<Web.Web>());
                }
                else
                {
                    Vector3 target = ray.GetPoint(100f);
                    _weapon.Shoot(target, _righthand.position,web.GetComponent<Web.Web>());
                }

                _canShoot = false;

                DOTween.Sequence().AppendInterval(0.3f).OnComplete(() =>
                {
                    _canShoot = true;
                    _gameObjectPoolProvider.GameObjectPool.Push(web);
                });
            }
        }
    }
}