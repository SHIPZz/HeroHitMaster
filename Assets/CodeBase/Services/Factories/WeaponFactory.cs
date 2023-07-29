using System.Collections.Generic;
using CodeBase.Services.Providers;
using Constants;
using Enums;
using Gameplay.Weapons;
using ScriptableObjects;
using Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class WeaponFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private readonly LocationProvider _locationProvider;

        private readonly Dictionary<WeaponTypeId, string> _weapons;
        
        public WeaponFactory(AssetProvider assetProvider, DiContainer diContainer, 
            LocationProvider locationProvider, 
            WeaponSettings weaponSettings)
        {
            _weapons = weaponSettings.WeaponPathes;
            _locationProvider = locationProvider;
            _assetProvider = assetProvider;
            _diContainer = diContainer;
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
            return _diContainer.InstantiatePrefabForComponent<Weapon>(gunPrefab, _locationProvider.WeaponsParent);
        }
    }
}