using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Services.CheckOut;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.UI.Windows.Popup;
using Zenject;

namespace CodeBase.UI.Weapons
{
    class WeaponSelectorPresenter : IInitializable, IDisposable
    {
        private readonly WeaponSelector _weaponSelector;
        private readonly Dictionary<WeaponTypeId, WeaponSelectorView> _weaponIconClickers;
        private readonly CheckOutService _checkOutService;
        private readonly List<WeaponSelectorView> _popupWeaponIcons;

        public WeaponSelectorPresenter(WeaponIconsProvider weaponIconsProvider, WeaponSelector weaponSelector,
            CheckOutService checkOutService, IProvider<WeaponIconsProvider> provider)
        {
            _popupWeaponIcons = provider.Get().Icons.Values.ToList();
            _checkOutService = checkOutService;
            _weaponIconClickers = weaponIconsProvider.Icons;
            _weaponSelector = weaponSelector;
            _popupWeaponIcons.ForEach(x => x.Choosed += _weaponSelector.SetLastWeaponChoosed);
        }

        public void Initialize()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosed +=  _weaponSelector.SetLastWeaponChoosed;

            _checkOutService.Succeeded += _weaponSelector.Select;
        }

        public void Dispose()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosed -=  _weaponSelector.SetLastWeaponChoosed;
                
            _checkOutService.Succeeded -= _weaponSelector.Select;
            _popupWeaponIcons.ForEach(x => x.Choosed -= _weaponSelector.SetLastWeaponChoosed);
        }
    }
}