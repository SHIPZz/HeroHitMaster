using System;
using System.Collections.Generic;
using Constants;
using Databases;
using Enums;
using Gameplay.Character.Player.Shoot;
using Gameplay.Web;
using Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace Services.Factories
{
    public class WeaponFactory 
    {
        private readonly AssetProvider _assetProvider;
        private readonly WeaponsProvider _weaponsProvider;
        private readonly WeaponSelector _weaponSelector;
        private readonly DiContainer _diContainer;
        private readonly Dictionary<WeaponTypeId, string> _weapons;

        public WeaponFactory(AssetProvider assetProvider, WeaponsProvider weaponsProvider, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _weaponsProvider = weaponsProvider;
            _diContainer = diContainer;
            _weapons = new Dictionary<WeaponTypeId, string>()
            {
                {WeaponTypeId.ShootSpiderHand, AssetPath.SpiderWebGun},
                {WeaponTypeId.ShootWolverineHand, AssetPath.WolverineWeb}
            };
        }

        public IWeapon Create()
        {
            if (!_weapons.TryGetValue(_weaponsProvider.CurrentWeapon.WeaponTypeId, out var prefabPath))
            {
                Debug.Log("ERROR");
                return null;
            }

            return Create(prefabPath);
        }

        private IWeapon Create(string prefabGunPath)
        {
            var gunPrefab = _assetProvider.GetAsset<ShootHand>(prefabGunPath);
            return _diContainer.InstantiatePrefabForComponent<ShootHand>(gunPrefab);
        }
    }
}