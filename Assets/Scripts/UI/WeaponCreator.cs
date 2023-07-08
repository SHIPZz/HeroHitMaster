using System;
using Enums;
using Gameplay.Weapon;
using Services.Factories;
using Services.Providers;

namespace UI
{
    public class WeaponCreator
    {
        private readonly WeaponFactory _weaponFactory;
        private readonly WeaponsProvider _weaponsProvider;
        private readonly PlayerProvider _playerProvider;

        public event Action<Weapon> OldWeaponChanged;
        public event Action<Weapon> NewWeaponChanged;

        public WeaponCreator(WeaponFactory weaponFactory,  WeaponsProvider weaponsProvider, PlayerProvider playerProvider)
        {
            _weaponFactory = weaponFactory;
            _weaponsProvider = weaponsProvider;
            _playerProvider = playerProvider;
        }

        public void CreateWeapon(WeaponTypeId weaponTypeId)
        { 
            var weapon =   _weaponFactory.Create(weaponTypeId, _playerProvider.CurrentPlayer.gameObject.transform);
           _weaponsProvider.CurrentWeapon = weapon;
        }
    }
}