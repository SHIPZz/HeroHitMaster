using System;
using System.Collections.Generic;
using Constants;
using Enums;
using Gameplay.Character.Player.Shoot;
using Gameplay.Weapon;
using Gameplay.Web;
using Services.Providers;
using Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace Services.Factories
{
    public class WeaponFactory 
    {
        private readonly AssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private readonly Dictionary<WeaponTypeId, string> _weapons;

        public WeaponFactory(AssetProvider assetProvider, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
            _weapons = new Dictionary<WeaponTypeId, string>()
            {
                {WeaponTypeId.ShootSpiderHand, AssetPath.SpiderWebGun},
                {WeaponTypeId.ShootWolverineHand, AssetPath.WolverineWebGun}
            };
        }

        public Weapon Create(WeaponTypeId weaponTypeId)
        {
            if (!_weapons.TryGetValue(weaponTypeId, out var prefabPath))
            {
                Debug.Log("ERROR");
                return null;
            }

            return Create(prefabPath);
        }

        private Weapon Create(string prefabGunPath)
        {
            var gunPrefab = _assetProvider.GetAsset<Weapon>(prefabGunPath);
            return _diContainer.InstantiatePrefabForComponent<Weapon>(gunPrefab);
        }
    }
}