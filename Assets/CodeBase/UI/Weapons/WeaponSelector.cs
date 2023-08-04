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
        private readonly WeaponProvider _weaponProvider;
        private readonly IWeaponStorage _weaponStorage;

        public event Action<Weapon> OldWeaponChanged;
        public event Action<WeaponTypeId> NewWeaponChanged;

        public WeaponSelector(WeaponProvider weaponProvider,
            IWeaponStorage weaponStorage)
        {
            _weaponProvider = weaponProvider;
            _weaponStorage = weaponStorage;
        }

        public void Select(WeaponTypeId weaponTypeId)
        {
            var weapon = _weaponStorage.Get(weaponTypeId);
            _weaponProvider.CurrentWeapon = weapon;
            NewWeaponChanged?.Invoke(weaponTypeId);
        }
    }
}