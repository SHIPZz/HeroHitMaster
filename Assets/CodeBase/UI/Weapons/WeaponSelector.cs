using System;
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
        private WeaponTypeId _lastWeaponId;
        
        public event Action<WeaponTypeId> NewWeaponChanged;

        public WeaponSelector(IProvider<Weapon> weaponProvider,
            IWeaponStorage weaponStorage)
        {
            _weaponProvider = weaponProvider;
            _weaponStorage = weaponStorage;
        }

        public void Select()
        {
            Weapon weapon = _weaponStorage.Get(_lastWeaponId);
            _weaponProvider.Set(weapon);
            NewWeaponChanged?.Invoke(_lastWeaponId);
        }

        public void SetLastWeaponChoosed(WeaponTypeId weaponTypeId) => 
            _lastWeaponId = weaponTypeId;
    }
}