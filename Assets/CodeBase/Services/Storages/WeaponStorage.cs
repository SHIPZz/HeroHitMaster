using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.ScriptableObjects;
using CodeBase.Services.Factories;
using UnityEngine;

namespace CodeBase.Services.Storages
{
    public class WeaponStorage : IWeaponStorage
    {
        private readonly WeaponFactory _weaponFactory;
        private readonly Dictionary<WeaponTypeId, Weapon> _weapons = new();

        public WeaponStorage(WeaponSettings weaponSettings, WeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
            FillDictionary(weaponSettings.WeaponTypeIds);
            SetActiveAll(false);
        }

        public Weapon Get(WeaponTypeId weaponTypeId)
        {
            SetActiveAll(false);
            SetActive(_weapons[weaponTypeId], true);
            return  _weapons[weaponTypeId];
        }

        private void SetActive(Weapon weapon, bool isActive) =>
            weapon.gameObject.SetActive(isActive);

        private void SetActiveAll(bool isActive)
        {
            foreach (var weapon in _weapons.Values)
            {
                weapon.gameObject.SetActive(isActive);
            }
        }

        private void FillDictionary(List<WeaponTypeId> weaponTypeIds)
        {
            foreach (var weaponSetting in weaponTypeIds)
            {
                Weapon weapon = _weaponFactory.Create(weaponSetting);
                _weapons[weapon.WeaponTypeId] = weapon;
            }
        }
    }
}