using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Bullet.DamageDealers;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.ObjectPool;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Camera
{
    public class CameraShakeMediator
    { 
        private CameraShake _cameraShake;
        private IProvider<Weapon> _provider;
        private Weapon _weapon;
        private IProvider<Dictionary<WeaponTypeId, GameObjectPool>> _bulletsPoolProvider;


        private List<WeaponTypeId> _recoilWeapons = new()
        {
            { WeaponTypeId.BlueWeapon },
            { WeaponTypeId.GreenWeapon },
            { WeaponTypeId.OrangeWeapon },
        };

        public CameraShakeMediator(IProvider<WeaponProvider> provider,
            IProvider<Dictionary<WeaponTypeId, GameObjectPool>> bulletsPoolProvider)
        {
            _bulletsPoolProvider = bulletsPoolProvider;
            provider.Get().Changed += SetWeapon;
        }

        private void SetWeapon(Weapon weapon) =>
            _weapon = weapon;

        public void SetCamerShake(CameraShake cameraShake) =>
            _cameraShake = cameraShake;

        public async void Init()
        {
            while (!_weapon.Initialized)
            {
                await UniTask.Yield();
            }

            if (_recoilWeapons.Contains(_weapon.WeaponTypeId))
                _weapon.Shooted += _cameraShake.MakeRecoil;

            if (_weapon.WeaponTypeId != WeaponTypeId.ThrowingDynamiteShooter)
                return;

            List<GameObject> bullets = _bulletsPoolProvider.Get()[_weapon.WeaponTypeId].GetAll();

            foreach (GameObject gameObject in bullets)
            {
                var bullet = gameObject.GetComponent<DestructionDamageDealer>();
                bullet.Done += MakeShake;
            }
        }

        private void MakeShake() => 
            _cameraShake.MakeShake(2);
    }
}