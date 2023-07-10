using System;
using Enums;
using Gameplay.Weapon;
using Services.Factories;
using Services.Providers;
using Services.Storages;

namespace UI
{
    public class WeaponSelector
    {
        private readonly WeaponFactory _weaponFactory;
        private readonly WeaponsProvider _weaponsProvider;
        private readonly PlayerProvider _playerProvider;
        private readonly IWeaponStorage _weaponStorage;

        public event Action<Weapon> OldWeaponChanged;
        public event Action<Weapon> NewWeaponChanged;

        public WeaponSelector(WeaponsProvider weaponsProvider, PlayerProvider playerProvider, IWeaponStorage weaponStorage)
        {
            _weaponsProvider = weaponsProvider;
            _playerProvider = playerProvider;
            _weaponStorage = weaponStorage;
        }

        public void CreateWeapon(WeaponTypeId weaponTypeId)
        { 
            Weapon weapon =   _weaponStorage.Get(weaponTypeId);
            weapon.gameObject.transform.SetParent(_playerProvider.CurrentPlayer.transform);
           _weaponsProvider.CurrentWeapon = weapon;
        }
    }
}