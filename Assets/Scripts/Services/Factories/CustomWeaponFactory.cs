using System;
using Constants;
using Databases;
using Enums;
using Gameplay.Character.Player.Shoot;
using Gameplay.Web;
using Services.Providers.AssetProviders;
using Zenject;

namespace Services.Factories
{
    public class CustomWeaponFactory : IFactory<IWeapon>
    {
        private readonly AssetProvider _assetProvider;
        private readonly WeaponsProvider _weaponsProvider;
        private readonly WeaponSelector _weaponSelector;
        private readonly DiContainer _diContainer;

        public CustomWeaponFactory(AssetProvider assetProvider, WeaponsProvider weaponsProvider, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _weaponsProvider = weaponsProvider;
            // _weaponSelector = weaponSelector;
            _diContainer = diContainer;
        }

        public IWeapon Create()
        {
            switch (_weaponsProvider.CurrentWeapon.WeaponTypeId)
            {
                case WeaponTypeId.ShootSpiderHand:
                    return CreateShootSpiderHand();
                
                case WeaponTypeId.ShootWolverineHand:
                    return CreateShootWolverineHand();
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IWeapon CreateShootWolverineHand()
        {
            var wolverineWebGun = _assetProvider.GetAsset<ShootHand>(AssetPath.WolverineWebGun);
            return _diContainer.InstantiatePrefabForComponent<ShootHand>(wolverineWebGun);
        }

        private IWeapon CreateShootSpiderHand()
        {
           var spiderWebGun = _assetProvider.GetAsset<ShootHand>(AssetPath.SpiderWebGun);
           return _diContainer.InstantiatePrefabForComponent<ShootHand>(spiderWebGun);
        }
    }
}