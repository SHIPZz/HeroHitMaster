using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.CheckOut;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.UI.Weapons;
using Zenject;

namespace CodeBase.UI.Windows.Buy
{
    public class BuyWeaponPresenter : IInitializable, IDisposable
    {
        private readonly BuyButtonView _buyButtonView;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private readonly List<WeaponSelectorView> _selectorViews;
        private readonly CheckOutService _checkOutService;
        private readonly AdCheckOutService _adCheckOutService;
        private readonly AdBuyButtonView _adBuyButtonView;
        private WeaponTypeId _weaponId;

        public BuyWeaponPresenter(BuyButtonView buyButtonView, 
            IProvider<List<WeaponSelectorView>> weaponSelectorViewsProvider,
            WeaponStaticDataService weaponStaticDataService, CheckOutService checkOutService,
            AdCheckOutService adCheckOutService, AdBuyButtonView adBuyButtonView)
        {
            _adBuyButtonView = adBuyButtonView;
            _adCheckOutService = adCheckOutService;
            _checkOutService = checkOutService;
            _selectorViews = weaponSelectorViewsProvider.Get();
            _weaponStaticDataService = weaponStaticDataService;
            _buyButtonView = buyButtonView;
        }

        public void Initialize()
        {
            _buyButtonView.Clicked += InvokeCheckOutService;
            _adBuyButtonView.Clicked += InvokeAdCheckOutService;
            _selectorViews.ForEach(x=> x.Choosed += SetLastChoosedWeapon);
        }

        public void Dispose()
        {
            _buyButtonView.Clicked -= InvokeCheckOutService;
            _adBuyButtonView.Clicked -= InvokeAdCheckOutService;
            _selectorViews.ForEach(x=> x.Choosed -= SetLastChoosedWeapon);
        }

        private void InvokeAdCheckOutService()
        {
            var adCount = _weaponStaticDataService.Get(_weaponId).Price.AdQuantity;
            _adCheckOutService.Buy(adCount);
        }

        private void SetLastChoosedWeapon(WeaponTypeId weaponTypeId) => 
            _weaponId = weaponTypeId;

        private void InvokeCheckOutService()
        {
            var cost = _weaponStaticDataService.Get(_weaponId).Price.Value;
            
            _checkOutService.Buy(cost);
        }
    }
}