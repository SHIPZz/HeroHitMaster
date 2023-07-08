using System;
using System.Collections.Generic;
using Enums;
using Zenject;

namespace UI
{
    class WeaponSelectorPresenter : IInitializable, IDisposable
    {
        private readonly WeaponCreator _weaponSelector;
        private readonly Dictionary<WeaponTypeId, WeaponSelectorView> _weaponIconClickers;

        public WeaponSelectorPresenter(WeaponIconsProvider weaponIconsProvider, WeaponCreator weaponSelector)
        {
            _weaponIconClickers = weaponIconsProvider.Icons;
            _weaponSelector = weaponSelector;
        }

        public void Initialize()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosed += _weaponSelector.CreateWeapon;
        }

        public void Dispose()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosed -= _weaponSelector.CreateWeapon;
        }
    }
    
    
}