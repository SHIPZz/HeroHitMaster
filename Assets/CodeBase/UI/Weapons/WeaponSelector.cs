using System;
using System.Threading.Tasks;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.CheckOut;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.Storages.Weapon;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Weapons
{
    public class WeaponSelector
    {
        private const int WeaponPopupLvlDuration = 5;
    
        private readonly IProvider<Weapon> _weaponProvider;
        private readonly IWeaponStorage _weaponStorage;
        private readonly ISaveSystem _saveSystem;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private WeaponTypeId _lastWeaponId;

        public WeaponTypeId LastWeaponId => _lastWeaponId;

        public event Action<WeaponTypeId> NewWeaponChanged;

        public WeaponSelector(IProvider<Weapon> weaponProvider,
            IWeaponStorage weaponStorage, ISaveSystem saveSystem, WeaponStaticDataService weaponStaticDataService)
        {
            _weaponStaticDataService = weaponStaticDataService;
            _saveSystem = saveSystem;
            _weaponProvider = weaponProvider;
            _weaponStorage = weaponStorage;
        }

        public async void Select()
        {
            await SaveLastSelectedWeapon();

            WeaponData weaponData = _weaponStaticDataService.Get(_lastWeaponId);

            if (weaponData.Price.PriceTypeId == PriceTypeId.Popup)
            {
                var levelData = await _saveSystem.Load<LevelData>();

                if (levelData.Id % WeaponPopupLvlDuration == 0)
                {
                    var playerData = await _saveSystem.Load<PlayerData>();
                    _lastWeaponId = playerData.LastNotPopupWeaponId;
                }
            }

            Weapon weapon = _weaponStorage.Get(_lastWeaponId);
            _weaponProvider.Set(weapon);

            NewWeaponChanged?.Invoke(_lastWeaponId);
        }

        public async void Select(WeaponTypeId weaponTypeId)
        {
            await SaveLastSelectedWeapon();

            Weapon weapon = _weaponStorage.Get(weaponTypeId);
            _weaponProvider.Set(weapon);

            NewWeaponChanged?.Invoke(weaponTypeId);
        }
        
        public async void SetLastWeaponChoosen(WeaponTypeId weaponTypeId)
        {
            _lastWeaponId = weaponTypeId;

            var playerData = await _saveSystem.Load<PlayerData>();

            if (_weaponStaticDataService.Get(weaponTypeId).Price.PriceTypeId == PriceTypeId.Popup)
            {
                Select();
                return;
            }

            if (!playerData.PurchasedWeapons.Contains(weaponTypeId))
                return;

            playerData.LastNotPopupWeaponId = _lastWeaponId;
            Select();
        }

        private async UniTask SaveLastSelectedWeapon()
        {
            var playerData = await _saveSystem.Load<PlayerData>();
            playerData.LastWeaponId = _lastWeaponId;

            _saveSystem.Save(playerData);
        }
    }
}