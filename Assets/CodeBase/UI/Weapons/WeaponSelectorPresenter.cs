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
using CodeBase.UI.Windows.Shop;
using Zenject;

namespace CodeBase.UI.Weapons
{
    class WeaponSelectorPresenter : IInitializable, IDisposable
    {
        private readonly WeaponSelector _weaponSelector;
        private readonly Dictionary<WeaponTypeId, WeaponSelectorView> _weaponIconClickers;
        private readonly CheckOutService _checkOutService;
        private readonly PopupInfoView _popupInfoView;
        private readonly WeaponAdWatchCounter _weaponAdWatchCounter;
        private ShopWeaponInfoView _shopWeaponInfoView;

        public WeaponSelectorPresenter(WeaponIconsProvider weaponIconsProvider, 
            WeaponSelector weaponSelector,
            CheckOutService checkOutService,  
            PopupInfoView popupInfoView,
            WeaponAdWatchCounter weaponAdWatchCounter,
            ShopWeaponInfoView shopWeaponInfoView)
        {
            _shopWeaponInfoView = shopWeaponInfoView;
            _weaponAdWatchCounter = weaponAdWatchCounter;
            _popupInfoView = popupInfoView;
            _checkOutService = checkOutService;
            _weaponIconClickers = weaponIconsProvider.Icons;
            _weaponSelector = weaponSelector;
        }

        public void Initialize()
        {
            _popupInfoView.LastWeaponSelected += _weaponSelector.SetLastPopupWeapon;
            _weaponAdWatchCounter.AllAdWatched += _weaponSelector.SetBoughtWeaponAdWeapon;
            
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosen +=  _weaponSelector.SetLastShopWeaponIdSelected;

            _checkOutService.Succeeded += _weaponSelector.SetBoughtMoneyWeapon;
            _shopWeaponInfoView.AcceptWeaponButtonClicked += _weaponSelector.SetLastShopWeaponSelected;
        }

        public void Dispose()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosen -=  _weaponSelector.SetLastShopWeaponIdSelected;
                
            _shopWeaponInfoView.AcceptWeaponButtonClicked -= _weaponSelector.SetLastShopWeaponSelected;
            _weaponAdWatchCounter.AllAdWatched -= _weaponSelector.SetBoughtWeaponAdWeapon;
            _checkOutService.Succeeded -= _weaponSelector.SetBoughtMoneyWeapon;
            _popupInfoView.LastWeaponSelected -= _weaponSelector.SetLastPopupWeapon;
        }
    }
}