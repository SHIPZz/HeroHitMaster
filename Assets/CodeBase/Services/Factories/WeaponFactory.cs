using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.ScriptableObjects;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Providers;
using CodeBase.Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class WeaponFactory
    {
        private readonly DiContainer _diContainer;
        private readonly LocationProvider _locationProvider;

        private readonly Dictionary<WeaponTypeId, Weapon> _weapons;
        
        public WeaponFactory(AssetProvider assetProvider, DiContainer diContainer, LocationProvider locationProvider)
        {
            _weapons = Resources.LoadAll<Weapon>("Prefabs/Gun")
                .ToDictionary(x => x.WeaponTypeId, x => x);
            _locationProvider = locationProvider;
            _diContainer = diContainer;
        }

        public Weapon Create(WeaponTypeId weaponTypeId)
        {            
            Weapon gunPrefab = _weapons[weaponTypeId];
            return _diContainer.InstantiatePrefabForComponent<Weapon>(gunPrefab, _locationProvider.WeaponsParent);
        }
        
    }
}