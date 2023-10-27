using System.Collections.Generic;
using CodeBase.Constants;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Data;
using CodeBase.Services.ObjectPool;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Storages.Bullet
{
    public class BulletStorage
    {
        private readonly  IProvider<Dictionary<WeaponTypeId, GameObjectPool>> _bulletsPoolProvider;
        private readonly DiContainer _diContainer;
        private readonly IProvider<Player> _playerProvider;
        private readonly IProvider<LocationTypeId, Transform> _locationProvider;
        private readonly BulletStaticDataService _bulletStaticDataService;

        public BulletStorage(IProvider<Dictionary<WeaponTypeId, GameObjectPool>> bulletsPoolProvider,
            DiContainer diContainer, IProvider<Player> playerProvider,
            IProvider<LocationTypeId, Transform> locationProvider,
            BulletStaticDataService bulletStaticDataService)
        {
            _bulletStaticDataService = bulletStaticDataService;
            _locationProvider = locationProvider;
            _bulletsPoolProvider = bulletsPoolProvider;
            _diContainer = diContainer;
            _playerProvider = playerProvider;
        }

        public async UniTask CreateBulletsBy(WeaponTypeId weaponTypeId)
        {
            var bulletPrefab = _bulletStaticDataService.GetBy(weaponTypeId).Prefab.GetComponent<Gameplay.Bullet.Bullet>();
            Vector3 rightHandPosition = _playerProvider.Get().RightHand.position;

            var count = _bulletStaticDataService.GetBy(bulletPrefab.WeaponTypeId).Count;
            
            _bulletsPoolProvider.Get()[bulletPrefab.WeaponTypeId] = new GameObjectPool(() =>
                _diContainer.InstantiatePrefab(bulletPrefab,
                    rightHandPosition, Quaternion.identity, 
                    _locationProvider.Get(LocationTypeId.BulletParent)), count);

            while (_bulletsPoolProvider.Get()[bulletPrefab.WeaponTypeId].GetAll().Count < count)
            {
                await UniTask.Yield();
            }
        }

        public Gameplay.Bullet.Bullet Pop(WeaponTypeId weaponTypeId)
        {
            var bullet = _bulletsPoolProvider.Get()[weaponTypeId].Pop().GetComponent<Gameplay.Bullet.Bullet>();
            bullet.transform.position = _playerProvider.Get().RightHand.position;
            return bullet;
        }

        public void Push(Gameplay.Bullet.Bullet bullet)
        {
            if (bullet == null)
                return;
            
            bullet.transform.position = _playerProvider.Get().RightHand.position;
            bullet.transform.SetParent(_locationProvider.Get(LocationTypeId.BulletParent));
            bullet.GetComponent<Rigidbody>().isKinematic = false;
            bullet.gameObject.layer = LayerId.Bullet;
            _bulletsPoolProvider.Get()[bullet.WeaponTypeId].Push(bullet.gameObject);
        }
    }
}