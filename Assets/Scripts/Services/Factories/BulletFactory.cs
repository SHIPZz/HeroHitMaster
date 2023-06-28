using System;
using System.Collections.Generic;
using Constants;
using Enums;
using Gameplay.Bullet;
using Gameplay.Web;
using ModestTree.Util;
using Services.ObjectPool;
using Services.Providers;
using Services.Providers.AssetProviders;
using UnityEngine;
using Object = UnityEngine.Object;
using Vector3 = System.Numerics.Vector3;

namespace Services.Factories
{
    public class BulletFactory
    {
        private const int Count = 50;

        private Dictionary<WeaponTypeId, string> _bullets;
        private readonly AssetProvider _assetProvider;
        private readonly GameObjectPoolProvider _gameObjectPoolProvider;

        public BulletFactory(AssetProvider assetProvider, GameObjectPoolProvider gameObjectPoolProvider)
        {
            _assetProvider = assetProvider;
            _gameObjectPoolProvider = gameObjectPoolProvider;

            _bullets = new Dictionary<WeaponTypeId, string>()
            {
                { WeaponTypeId.ShootSpiderHand, AssetPath.SpiderWeb },
                { WeaponTypeId.ShootWolverineHand, AssetPath.WolverineWeb }
            };
        }

        public void CreateBulletsBy(WeaponTypeId weaponTypeId, Transform parent)
        {
            if (!_bullets.TryGetValue(weaponTypeId, out var prefabPath))
            {
                Debug.Log("Wrong prefabPath");
                return;
            }

            Create(prefabPath, parent);
        }

        public IBullet Pop() =>
            _gameObjectPoolProvider.GameObjectPool.Pop().GetComponent<IBullet>();

        public void Push(IBullet bullet) =>
            _gameObjectPoolProvider.GameObjectPool.Push(bullet.GameObject);

        private void Create(string prefabPath, Transform parent)
        {
            GameObject bulletPrefab = _assetProvider.GetAsset(prefabPath);
            _gameObjectPoolProvider.GameObjectPool = new GameObjectPool(() => Object.Instantiate(bulletPrefab, parent), Count);
        }
    }
}