﻿using System;
using Enums;
using Gameplay.Weapon;
using Services.Factories;
using Services.Providers;
using Services.Storages;
using UnityEngine;

namespace Weapons
{
    public class WeaponSelector
    {
        private readonly WeaponFactory _weaponFactory;
        private readonly WeaponProvider _weaponProvider;
        private readonly PlayerProvider _playerProvider;
        private readonly IWeaponStorage _weaponStorage;

        public event Action<Weapon> OldWeaponChanged;
        public event Action<WeaponTypeId> NewWeaponChanged;

        public WeaponSelector(WeaponProvider weaponProvider, PlayerProvider playerProvider,
            IWeaponStorage weaponStorage)
        {
            _weaponProvider = weaponProvider;
            _playerProvider = playerProvider;
            _weaponStorage = weaponStorage;
        }

        public void CreateWeapon(WeaponTypeId weaponTypeId)
        {
            var weapon = _weaponStorage.Get(weaponTypeId);
            _weaponProvider.CurrentWeapon = weapon;
            NewWeaponChanged?.Invoke(weaponTypeId);
        }
    }
}