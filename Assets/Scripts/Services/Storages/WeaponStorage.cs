using System.Collections.Generic;
using Enums;
using Gameplay.Weapons;
using ScriptableObjects;
using Services.Factories;

namespace Services.Storages
{
    public class WeaponStorage : IWeaponStorage
    {
        private readonly WeaponFactory _weaponFactory;
        private readonly Dictionary<WeaponTypeId, Weapon> _weapons = new();

        public WeaponStorage(List<WeaponSettings> weaponSettings, WeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
            FillDictionary(weaponSettings);
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

        private void FillDictionary(List<WeaponSettings> weaponSettingsList)
        {
            foreach (var weaponSetting in weaponSettingsList)
            {
                Weapon weapon = _weaponFactory.Create(weaponSetting.WeaponTypeId, null);
                _weapons[weapon.WeaponTypeId] = weapon;
            }
        }
    }
}