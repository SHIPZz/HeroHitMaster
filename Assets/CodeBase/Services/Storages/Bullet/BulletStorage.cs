using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Gameplay.Bullet;
using CodeBase.Installers.ScriptableObjects.Gun;
using CodeBase.ScriptableObjects.Bullet;
using CodeBase.Services.Data;
using CodeBase.Services.ObjectPool;
using CodeBase.Services.Providers;
using CodeBase.Services.Providers.AssetProviders;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Storages.Bullet
{
    public class BulletStorage
    {
        private readonly GameObjectPoolProvider _gameObjectPoolProvider;
        private readonly DiContainer _diContainer;
        private readonly PlayerProvider _playerProvider;
        private readonly LocationProvider _locationProvider;
        private readonly BulletStaticDataService _bulletStaticDataService;

        public BulletStorage(GameObjectPoolProvider gameObjectPoolProvider,
            DiContainer diContainer, PlayerProvider playerProvider, LocationProvider locationProvider,BulletStaticDataService bulletStaticDataService)
        {
            _bulletStaticDataService = bulletStaticDataService;
            _locationProvider = locationProvider;
            _gameObjectPoolProvider = gameObjectPoolProvider;
            _diContainer = diContainer;
            _playerProvider = playerProvider;
        }

        public void CreateBulletsBy(WeaponTypeId weaponTypeId)
        {
            IBullet bulletPrefab = _bulletStaticDataService.GetBy(weaponTypeId).Prefab.GetComponent<IBullet>();
            Vector3 rightHandPosition = _playerProvider.CurrentPlayer.RightHand.position;

            var count = _bulletStaticDataService.GetBy(bulletPrefab.BulletTypeId).Count;
            
            _gameObjectPoolProvider.BulletPools[bulletPrefab.BulletTypeId] = new GameObjectPool(() =>
                _diContainer.InstantiatePrefab(bulletPrefab.GameObject,
                    rightHandPosition, Quaternion.identity, 
                    _locationProvider.Values[LocationTypeId.BulletParent]), count);
        }

        public IBullet Pop(WeaponTypeId weaponTypeId)
        {
            BulletTypeId targetBulletId = _bulletStaticDataService.GetBy(weaponTypeId).BulletTypeId;
            return _gameObjectPoolProvider.BulletPools[targetBulletId].Pop().GetComponent<IBullet>();
        }

        public void Push(IBullet bullet) =>
            _gameObjectPoolProvider.BulletPools[bullet.BulletTypeId].Push(bullet.GameObject);
    }
}