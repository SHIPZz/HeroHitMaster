using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.CheckOut;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using Zenject;

namespace CodeBase.UI.Weapons
{
    class WeaponSelectorPresenter : IInitializable, IDisposable
    {
        private readonly WeaponSelector _weaponSelector;
        private readonly Dictionary<WeaponTypeId, WeaponSelectorView> _weaponIconClickers;
        private CheckOutService _checkOutService;

        public WeaponSelectorPresenter(WeaponIconsProvider weaponIconsProvider, WeaponSelector weaponSelector,
            CheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
            _weaponIconClickers = weaponIconsProvider.Icons;
            _weaponSelector = weaponSelector;
        }

        public void Initialize()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosed += OnWeaponIconChoosed;

            _checkOutService.Succeeded += _weaponSelector.Select;
        }

        public void Dispose()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosed -= OnWeaponIconChoosed;
                
            _checkOutService.Succeeded -= _weaponSelector.Select;
        }

        private void OnWeaponIconChoosed(WeaponTypeId weaponTypeId)
        {
            _weaponSelector.SetLastWeaponChoosed(weaponTypeId);
        }
    }
}