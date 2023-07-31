using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Bullet;
using CodeBase.Installers.ScriptableObjects.Gun;
using CodeBase.Services.ObjectPool;
using CodeBase.Services.Providers;
using CodeBase.Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class BulletFactory
    {
        private readonly Dictionary<WeaponTypeId, string> _bulletsPathesByWeapon;
        private readonly Dictionary<WeaponTypeId, BulletTypeId> _bulletIdsByWeapons;

        private readonly AssetProvider _assetProvider;
        private readonly GameObjectPoolProvider _gameObjectPoolProvider;
        private readonly DiContainer _diContainer;
        private readonly PlayerProvider _playerProvider;
        private readonly LocationProvider _locationProvider;

        public BulletFactory(AssetProvider assetProvider, GameObjectPoolProvider gameObjectPoolProvider,
            DiContainer diContainer, PlayerProvider playerProvider, LocationProvider locationProvider,
            BulletSettings bulletSettings)
        {
            _bulletsPathesByWeapon = bulletSettings.BulletPathesByWeapon;
            _bulletIdsByWeapons = bulletSettings.BulletsByWeapon;
            _locationProvider = locationProvider;
            _assetProvider = assetProvider;
            _gameObjectPoolProvider = gameObjectPoolProvider;
            _diContainer = diContainer;
            _playerProvider = playerProvider;
        }

        public void CreateBulletsBy(WeaponTypeId weaponTypeId, int count)
        {
            if (!_bulletsPathesByWeapon.TryGetValue(weaponTypeId, out var prefabPath))
            {
                Debug.Log("Wrong prefabPath");
                return;
            }

            Create(prefabPath, count);
        }

        public IBullet Pop(WeaponTypeId weaponTypeId)
        {
            BulletTypeId targetBulletId = _bulletIdsByWeapons[weaponTypeId]; 
            return _gameObjectPoolProvider.BulletPools[targetBulletId].Pop().GetComponent<IBullet>();
        }

        public void Push(IBullet bullet) => 
            _gameObjectPoolProvider.BulletPools[bullet.BulletTypeId].Push(bullet.GameObject);

        private void Create(string prefabPath, int count)
        {
            var bulletPrefab = _assetProvider.GetAsset<IBullet>(prefabPath);

            Vector3 rightHandPosition = _playerProvider.CurrentPlayer.RightHand.position;
            
            _gameObjectPoolProvider.BulletPools[bulletPrefab.BulletTypeId] = new GameObjectPool(() =>
                _diContainer.InstantiatePrefab(bulletPrefab.GameObject,
                    rightHandPosition, Quaternion.identity, _locationProvider.BulletParent), count);            
        }
        
    }
}