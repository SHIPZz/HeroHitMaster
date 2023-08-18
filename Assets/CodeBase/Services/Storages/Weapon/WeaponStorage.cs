using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Data;
using CodeBase.Services.Factories;

namespace CodeBase.Services.Storages.Weapon
{
    public class WeaponStorage : IWeaponStorage
    {
        private readonly WeaponFactory _weaponFactory;
        private readonly Dictionary<WeaponTypeId, Gameplay.Weapons.Weapon> _weapons = new();

        public WeaponStorage(WeaponStaticDataService weaponStaticData, WeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
            FillDictionary(weaponStaticData);
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

        private void FillDictionary(WeaponStaticDataService weaponStaticDataService)
        {
            foreach (var weaponData in weaponStaticDataService.GetAll())
            {
                Gameplay.Weapons.Weapon weapon = _weaponFactory.Create(weaponData.WeaponTypeId);
                _weapons[weapon.WeaponTypeId] = weapon;
            }
        }
    }
}