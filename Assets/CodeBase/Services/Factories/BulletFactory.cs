using System.Collections.Generic;
using CodeBase.Services.Providers;
using Constants;
using Enums;
using Gameplay.Bullet;
using Services.ObjectPool;
using Services.Providers;
using Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class BulletFactory
    {
        private readonly Dictionary<WeaponTypeId, string> _bullets;

        private readonly AssetProvider _assetProvider;
        private readonly GameObjectPoolProvider _gameObjectPoolProvider;
        private readonly DiContainer _diContainer;
        private readonly PlayerProvider _playerProvider;
        private readonly LocationProvider _locationProvider;

        public BulletFactory(AssetProvider assetProvider, GameObjectPoolProvider gameObjectPoolProvider,
            DiContainer diContainer, PlayerProvider playerProvider, LocationProvider locationProvider,
            BulletSettings bulletSettings)
        {
            _bullets = bulletSettings.BulletPathes;
            _locationProvider = locationProvider;
            _assetProvider = assetProvider;
            _gameObjectPoolProvider = gameObjectPoolProvider;
            _diContainer = diContainer;
            _playerProvider = playerProvider;
        }

        public void CreateBulletsBy(WeaponTypeId weaponTypeId, int count)
        {
            if (!_bullets.TryGetValue(weaponTypeId, out var prefabPath))
            {
                Debug.Log("Wrong prefabPath");
                return;
            }

            Create(prefabPath, count);
        }

        public IBullet Pop() =>
            _gameObjectPoolProvider.GameObjectPool.Pop().GetComponent<IBullet>();

        public void Push(IBullet bullet) =>
            _gameObjectPoolProvider.GameObjectPool.Push(bullet.GameObject);

        private void Create(string prefabPath, int count)
        {
            GameObject bulletPrefab = _assetProvider.GetAsset(prefabPath);

            _gameObjectPoolProvider.GameObjectPool = new GameObjectPool(() =>
                _diContainer.InstantiatePrefab(bulletPrefab, _playerProvider.CurrentPlayer.RightHand.transform.position,
                    Quaternion.identity,
                    _locationProvider.BulletParent), count);
        }
    }
}