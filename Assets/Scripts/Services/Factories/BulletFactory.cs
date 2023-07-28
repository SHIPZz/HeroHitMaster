using System.Collections.Generic;
using Constants;
using Enums;
using Gameplay.Bullet;
using Services.ObjectPool;
using Services.Providers;
using Services.Providers.AssetProviders;
using UnityEngine;
using Zenject; 

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

            FillDictionary();
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
                _diContainer.InstantiatePrefab(bulletPrefab, _playerProvider.CurrentPlayer.RightHand.transform.position, 
            Quaternion.identity,
                    parent), count);
        }

        private void FillDictionary()
        {
            _bullets = new Dictionary<WeaponTypeId, string>()
            {
                { WeaponTypeId.WebSpiderShooter, AssetPath.SpiderWeb },
                { WeaponTypeId.SmudgeWebShooter, AssetPath.SmudgeWeb },
                { WeaponTypeId.FireBallShooter, AssetPath.FireBall },
                { WeaponTypeId.SharpWebShooter, AssetPath.SharpWeb },
                { WeaponTypeId.ThrowingKnifeShooter, AssetPath.Knife },
                { WeaponTypeId.ThrowingIceCreamShooter, AssetPath.IceCream },
                { WeaponTypeId.ThrowingTridentShooter, AssetPath.Trident },
                { WeaponTypeId.ThrowingHammerShooter, AssetPath.Hammer },
            };
        }
    }
}