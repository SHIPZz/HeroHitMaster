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
using Zenject;
using Object = UnityEngine.Object;
using Vector3 = System.Numerics.Vector3;

namespace Services.Factories
{
    public class BulletFactory
    {
        private Dictionary<WeaponTypeId, string> _bullets;
        private readonly AssetProvider _assetProvider;
        private readonly GameObjectPoolProvider _gameObjectPoolProvider;
        private readonly DiContainer _diContainer;
        private readonly PlayerProvider _playerProvider;

        public BulletFactory(AssetProvider assetProvider, GameObjectPoolProvider gameObjectPoolProvider, 
            DiContainer diContainer, PlayerProvider playerProvider)
        {
            _assetProvider = assetProvider;
            _gameObjectPoolProvider = gameObjectPoolProvider;
            _diContainer = diContainer;
            _playerProvider = playerProvider;

            _bullets = new Dictionary<WeaponTypeId, string>()
            {
                { WeaponTypeId.WebSpiderShooter, AssetPath.SpiderWeb },
                { WeaponTypeId.SmudgeWebShooter, AssetPath.SmudgeWeb },
                { WeaponTypeId.FireBallShooter, AssetPath.FireBall },
                { WeaponTypeId.SharpWebShooter , AssetPath.SharpWeb}
            };
        }

        public void CreateBulletsBy(WeaponTypeId weaponTypeId, Transform parent, int count)
        {
            if (!_bullets.TryGetValue(weaponTypeId, out var prefabPath))
            {
                Debug.Log("Wrong prefabPath");
                return;
            }

            Create(prefabPath, parent, count);
        }

        public IBullet Pop() =>
            _gameObjectPoolProvider.GameObjectPool.Pop().GetComponent<IBullet>();

        public void Push(IBullet bullet) =>
            _gameObjectPoolProvider.GameObjectPool.Push(bullet.GameObject);

        private void Create(string prefabPath, Transform parent, int count)
        {
            GameObject bulletPrefab = _assetProvider.GetAsset(prefabPath);
            _gameObjectPoolProvider.GameObjectPool = new GameObjectPool(() =>
                _diContainer.InstantiatePrefab(bulletPrefab, _playerProvider.CurrentPlayer.Head.transform.position, 
            Quaternion.identity,
                    parent), count);
        }
    }
}