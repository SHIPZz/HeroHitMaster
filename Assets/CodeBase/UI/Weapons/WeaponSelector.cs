using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
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
        private readonly ISaveSystem _saveSystem;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private WeaponTypeId _lastWeaponId;

        public WeaponTypeId LastWeaponId => _lastWeaponId;

        public event Action<WeaponTypeId> NewWeaponChanged;

        public WeaponSelector(IProvider<Weapon> weaponProvider,
            IWeaponStorage weaponStorage, ISaveSystem saveSystem, 
            WeaponStaticDataService weaponStaticDataService)
        {
            _weaponStaticDataService = weaponStaticDataService;
            _saveSystem = saveSystem;
            _weaponProvider = weaponProvider;
            _weaponStorage = weaponStorage;
        }

        public async UniTask<Weapon> Init(WeaponTypeId lastWeaponId)
        {
            WeaponData weaponData = _weaponStaticDataService.Get(lastWeaponId);
            _lastWeaponId = lastWeaponId;

            if (weaponData.Price.PriceTypeId == PriceTypeId.Popup)
            {
                var levelData = await _saveSystem.Load<LevelData>();

                if (levelData.LevelsWithPopupWeapon != 0)
                {
                    if (levelData.LevelsWithPopupWeapon % WeaponPopupLvlDuration == 1)
                    {
                        var playerData = await _saveSystem.Load<PlayerData>();
                        _lastWeaponId = playerData.LastNotPopupWeaponId;
                        playerData.LastWeaponId = _lastWeaponId;
                        _saveSystem.Save(playerData);
                    }
                }
            }

            Weapon weapon = _weaponStorage.Get(_lastWeaponId);
            _weaponProvider.Set(weapon);
            NewWeaponChanged?.Invoke(_lastWeaponId);
            return weapon;
        }

        public async void Select()
        {
            await SaveLastSelectedWeapon();

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