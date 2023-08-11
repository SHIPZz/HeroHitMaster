using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Weapon;
using UnityEngine;

namespace CodeBase.UI.Weapons
{
    public class WeaponSelector
    {
        private readonly IProvider<Weapon> _weaponProvider;
        private readonly IWeaponStorage _weaponStorage;

        public event Action<Weapon> OldWeaponChanged;
        public event Action<WeaponTypeId> NewWeaponChanged;

        public WeaponSelector(IProvider<Weapon> weaponProvider,
            IWeaponStorage weaponStorage)
        {
            _weaponProvider = weaponProvider;
            _weaponStorage = weaponStorage;
        }

        public void Select(WeaponTypeId weaponTypeId)
        {
            var weapon = _weaponStorage.Get(weaponTypeId);
            _weaponProvider.Set(weapon);
            NewWeaponChanged?.Invoke(weaponTypeId);
        }
    }
}