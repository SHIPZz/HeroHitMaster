using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Services.CheckOut;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.UI.Weapons.ShopWeapons;
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
        private readonly PopupInfoView _popupInfoView;
        private readonly AdWatchCounter _adWatchCounter;

        public WeaponSelectorPresenter(WeaponIconsProvider weaponIconsProvider, WeaponSelector weaponSelector,
            CheckOutService checkOutService, 
            IProvider<WeaponIconsProvider> provider, PopupInfoView popupInfoView, AdWatchCounter adWatchCounter)
        {
            _adWatchCounter = adWatchCounter;
            _popupInfoView = popupInfoView;
            _popupWeaponIcons = provider.Get().Icons.Values.ToList();
            _checkOutService = checkOutService;
            _weaponIconClickers = weaponIconsProvider.Icons;
            _weaponSelector = weaponSelector;
        }

        public void Initialize()
        {
            _popupWeaponIcons.ForEach(x => x.Choosen += _weaponSelector.SetLastWeaponChoosen);
            _popupInfoView.LastWeaponSelected += _weaponSelector.SetLastWeaponChoosen;
            _adWatchCounter.AllAdWatched += _weaponSelector.Select;
            
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosen +=  _weaponSelector.SetLastWeaponChoosen;

            _checkOutService.Succeeded += _weaponSelector.Select;
        }

        public void Dispose()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosen -=  _weaponSelector.SetLastWeaponChoosen;
                
            _adWatchCounter.AllAdWatched -= _weaponSelector.Select;
            _checkOutService.Succeeded -= _weaponSelector.Select;
            _popupInfoView.LastWeaponSelected -= _weaponSelector.SetLastWeaponChoosen;
            _popupWeaponIcons.ForEach(x => x.Choosen -= _weaponSelector.SetLastWeaponChoosen);
        }
    }
}