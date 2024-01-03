using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.Storages.Weapon;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Weapons
{
    public class WeaponSelector
    {
        private const int WeaponPopupLvlDuration = 2;

        private readonly IProvider<Weapon> _weaponProvider;
        private readonly IWeaponStorage _weaponStorage;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private readonly IWorldDataService _worldDataService;
        private WeaponTypeId _lastWeaponId;

        public WeaponTypeId LastWeaponId => _lastWeaponId;

        public bool Initialized = false;

        public event Action<WeaponTypeId> NewWeaponChanged;


        public WeaponSelector(IProvider<Weapon> weaponProvider,
            IWeaponStorage weaponStorage, IWorldDataService worldDataService,
            WeaponStaticDataService weaponStaticDataService)
        {
            _worldDataService = worldDataService;
            _weaponStaticDataService = weaponStaticDataService;
            _weaponProvider = weaponProvider;
            _weaponStorage = weaponStorage;
        }

        public Weapon Init(WeaponTypeId lastWeaponId)
        {
            WeaponData weaponData = _weaponStaticDataService.Get(lastWeaponId);
            _lastWeaponId = lastWeaponId;

            if (weaponData.Price.PriceTypeId == PriceTypeId.Popup)
            {
                WorldData worldData = _worldDataService.WorldData;

                Debug.Log(worldData.LevelData.LevelsWithPopupWeapon);
                if (worldData.LevelData.LevelsWithPopupWeapon != 0)
                {
                    if (worldData.LevelData.LevelsWithPopupWeapon % WeaponPopupLvlDuration == 0)
                    {
                        _lastWeaponId = worldData.PlayerData.LastNotPopupWeaponId;
                        worldData.PlayerData.LastWeaponId = _lastWeaponId;
                    }
                }
            }

            Weapon weapon = _weaponStorage.Get(_lastWeaponId);
            _weaponProvider.Set(weapon);
            NewWeaponChanged?.Invoke(_lastWeaponId);
            return weapon;
        }

        public void SetBoughtMoneyWeapon()
        {
            SetWeapon(_lastWeaponId, false);
            SetLastNotPopupWeaponToData();
        }

        public void SetBoughtWeaponAdWeapon(WeaponTypeId weaponTypeId)
        {
            _lastWeaponId = weaponTypeId;
            SetWeapon(_lastWeaponId, false);
            SetLastNotPopupWeaponToData();
        }

        public void SetPopupWeapon(WeaponTypeId weaponTypeId)
        {
            _lastWeaponId = weaponTypeId;
            WorldData worldData = _worldDataService.WorldData;
            worldData.PlayerData.LastWeaponId = _lastWeaponId;
            SetWeapon(weaponTypeId,true);
            NewWeaponChanged?.Invoke(_lastWeaponId);
        }

        public void SetLastShopWeaponIdSelected(WeaponTypeId weaponTypeId) =>
            _lastWeaponId = weaponTypeId;

        public void SetLastShopWeaponSelected()
        {
            if (!_worldDataService.WorldData.PlayerData.PurchasedWeapons.Contains(_lastWeaponId)) 
                return;
                
            SetLastNotPopupWeaponToData();
            SetWeapon(_lastWeaponId, false);
        }

        private void SetWeapon(WeaponTypeId weaponTypeId, bool isPopup)
        {
            Weapon weapon = _weaponStorage.Get(weaponTypeId);
            WorldData worldData = _worldDataService.WorldData;
            worldData.PlayerData.LastWeaponId = weaponTypeId;

            if (!isPopup)
                worldData.PlayerData.LastNotPopupWeaponId = weaponTypeId;

            _weaponProvider.Set(weapon);
            NewWeaponChanged?.Invoke(weapon.WeaponTypeId);
        }

        private void SetLastNotPopupWeaponToData()
        {
            WorldData worldData = _worldDataService.WorldData;
            worldData.PlayerData.PurchasedWeapons.Add(_lastWeaponId);
            _worldDataService.Save();
        }
    }
}