using System;
using Enums;
using Services.Providers;

namespace Gameplay.WeaponSelection
{
    public class WeaponSelector
    {
        private readonly WeaponsProvider _weaponsProvider;
        private int _currentWeaponId;
        private WeaponTypeId _weaponTypeId;

        public event Action<WeaponTypeId> WeaponSelected;
        public event Action<WeaponTypeId> WeaponChanged;

        public WeaponSelector(WeaponsProvider weaponsProvider)
        {
            _weaponsProvider = weaponsProvider;
        }

        public void SaveCurrent()
        {
            _weaponTypeId = (WeaponTypeId)_currentWeaponId;
            _weaponsProvider.CharactersAvailableWeapons.Add(_weaponTypeId);
            WeaponSelected?.Invoke(_weaponTypeId);
        }

        public void SelectNext()
        {
            _currentWeaponId++;

            if (_currentWeaponId >= _weaponsProvider.WeaponConfigs.Count)
                _currentWeaponId = 0;

            WeaponChanged?.Invoke((WeaponTypeId)_currentWeaponId);
        }

        public void SelectPrevious()
        {
            _currentWeaponId--;

            if (_currentWeaponId < 0)
                _currentWeaponId = _weaponsProvider.WeaponConfigs.Count - 1;
        
            WeaponChanged?.Invoke((WeaponTypeId)_currentWeaponId);
        }
    }
}