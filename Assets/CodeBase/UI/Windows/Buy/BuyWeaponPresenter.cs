using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.CheckOut;
using CodeBase.Services.Data;
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
        private WeaponTypeId _weaponId;

        public BuyWeaponPresenter(BuyButtonView buyButtonView, 
            List<WeaponSelectorView> selectorViews,
            WeaponStaticDataService weaponStaticDataService, CheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
            _selectorViews = selectorViews;
            _weaponStaticDataService = weaponStaticDataService;
            _buyButtonView = buyButtonView;
        }

        public void Initialize()
        {
            _buyButtonView.Clicked += InvokeCheckOutService;
            _selectorViews.ForEach(x=> x.Choosed += SetLastChoosedWeapon);
        }

        public void Dispose()
        {
            _buyButtonView.Clicked -= InvokeCheckOutService;
            _selectorViews.ForEach(x=> x.Choosed -= SetLastChoosedWeapon);
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