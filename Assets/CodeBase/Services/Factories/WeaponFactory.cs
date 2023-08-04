using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.ScriptableObjects;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Data;
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
        
        private WeaponStaticDataService _weaponStaticDataService;

        public WeaponFactory(DiContainer diContainer, LocationProvider locationProvider, WeaponStaticDataService weaponStaticDataService)
        {
            _weaponStaticDataService = weaponStaticDataService;
            _locationProvider = locationProvider;
            _diContainer = diContainer;
        }

        public Weapon Create(WeaponTypeId weaponTypeId)
        {
            Weapon gunPrefab = _weaponStaticDataService.Get(weaponTypeId).Prefab;
            return _diContainer.InstantiatePrefabForComponent<Weapon>(gunPrefab, _locationProvider.WeaponsParent);
        }
        
    }
}