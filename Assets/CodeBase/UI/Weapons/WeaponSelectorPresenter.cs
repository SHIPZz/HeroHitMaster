using System;
using System.Collections.Generic;
using CodeBase.Enums;
using Zenject;

namespace CodeBase.UI.Weapons
{
    class WeaponSelectorPresenter : IInitializable, IDisposable
    {
        private readonly WeaponSelector _weaponSelector;
        private readonly Dictionary<WeaponTypeId, WeaponSelectorView> _weaponIconClickers;

        public WeaponSelectorPresenter(WeaponIconsProvider weaponIconsProvider, WeaponSelector weaponSelector)
        {
            _weaponIconClickers = weaponIconsProvider.Icons;
            _weaponSelector = weaponSelector;
        }

        public void Initialize()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosed += _weaponSelector.Select;
        }

        public void Dispose()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosed -= _weaponSelector.Select;
        }
    }
}