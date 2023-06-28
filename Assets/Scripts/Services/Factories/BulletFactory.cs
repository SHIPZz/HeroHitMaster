using Constants;
using Enums;
using Gameplay.Web;
using Services.ObjectPool;
using Services.Providers;
using Services.Providers.AssetProviders;
using UnityEngine;

namespace Services.Factories
{
    public class BulletFactory
    {
        private const int Count = 50;
        
        private readonly AssetProvider _assetProvider;
        private readonly GameObjectPoolProvider _gameObjectPoolProvider;

        public BulletFactory(AssetProvider assetProvider, GameObjectPoolProvider gameObjectPoolProvider)
        {
            _assetProvider = assetProvider;
            _gameObjectPoolProvider = gameObjectPoolProvider;
        }

        public void CreateBullet(WeaponTypeId weaponTypeId)
        {
            switch (weaponTypeId)
            {
                case WeaponTypeId.ShootSpiderHand:
                    CreateSpiderWeb();
                    break;
                
                case WeaponTypeId.ShootWolverineHand:
                    CreateWolverineWeb();
                    break;
            }
        }

        public IBullet Pop() =>
            _gameObjectPoolProvider.GameObjectPool.Pop().GetComponent<IBullet>();

        public void Push(IBullet bullet) => 
            _gameObjectPoolProvider.GameObjectPool.Push(bullet.GameObject);

        private void CreateWolverineWeb()
        {
            GameObject wolverineWebPrefab = _assetProvider.GetAsset(AssetPath.SmudgeWeb);
            _gameObjectPoolProvider.GameObjectPool = new GameObjectPool(() => Object.Instantiate(wolverineWebPrefab), Count);
        }

        private void CreateSpiderWeb()
        {
            GameObject webPrefab = _assetProvider.GetAsset(AssetPath.SpiderWeb);
            _gameObjectPoolProvider.GameObjectPool = new GameObjectPool(() => Object.Instantiate(webPrefab), Count);
        }
    }
}