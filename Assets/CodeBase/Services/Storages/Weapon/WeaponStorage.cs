using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Factories;

namespace CodeBase.Services.Storages.Weapon
{
    public class WeaponStorage : IWeaponStorage
    {
        private readonly WeaponFactory _weaponFactory;
        private readonly Dictionary<WeaponTypeId, Gameplay.Weapons.Weapon> _weapons = new();

        public WeaponStorage(WeaponSettings weaponSettings, WeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
            FillDictionary(weaponSettings.WeaponTypeIds);
            SetActiveAll(false);
        }

        public Gameplay.Weapons.Weapon Get(WeaponTypeId weaponTypeId)
        {
            SetActiveAll(false);
            SetActive(_weapons[weaponTypeId], true);
            return  _weapons[weaponTypeId];
        }

        private void SetActive(Gameplay.Weapons.Weapon weapon, bool isActive) =>
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
                Gameplay.Weapons.Weapon weapon = _weaponFactory.Create(weaponSetting);
                _weapons[weapon.WeaponTypeId] = weapon;
            }
        }
    }
}