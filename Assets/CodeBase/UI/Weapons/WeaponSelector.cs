using System;
using System.Threading.Tasks;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.CheckOut;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.Storages.Weapon;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Weapons
{
    public class WeaponSelector
    {
        private readonly IProvider<Weapon> _weaponProvider;
        private readonly IWeaponStorage _weaponStorage;
        private readonly ISaveSystem _saveSystem;
        private WeaponTypeId _lastWeaponId;
        private WeaponStaticDataService _weaponStaticDataService;

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
            
            Weapon weapon = _weaponStorage.Get(_lastWeaponId);
            _weaponProvider.Set(weapon);
            NewWeaponChanged?.Invoke(_lastWeaponId);
        }

        private async Task SaveLastSelectedWeapon()
        {
            var playerData = await _saveSystem.Load<PlayerData>();
            playerData.LastWeaponId = _lastWeaponId;
            _saveSystem.Save(playerData);
        }

        public async void SetLastWeaponChoosed(WeaponTypeId weaponTypeId)
        {
            _lastWeaponId = weaponTypeId;

            if (_weaponStaticDataService.Get(weaponTypeId).Price.PriceTypeId == PriceTypeId.Popup)
            {
                Select();
                return;
            }

            var playerData = await _saveSystem.Load<PlayerData>();

            if (playerData.PurchasedWeapons.Contains(weaponTypeId))
                Select();
        }
    }
}