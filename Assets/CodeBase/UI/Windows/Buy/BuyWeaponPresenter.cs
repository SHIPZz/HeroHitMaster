using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Data;
using CodeBase.UI.Weapons;
using Zenject;

namespace CodeBase.UI.Windows.Buy
{
    public class BuyWeaponPresenter : IInitializable, IDisposable
    {
        private readonly Wallet.Wallet _wallet;
        private readonly BuyButtonView _buyButtonView;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private readonly List<WeaponSelectorView> _selectorViews;
        private WeaponTypeId _weaponId;

        public event Action<WeaponTypeId> Succeeded;

        public BuyWeaponPresenter(Wallet.Wallet wallet, BuyButtonView buyButtonView, 
            List<WeaponSelectorView> selectorViews, WeaponStaticDataService weaponStaticDataService)
        {
            _selectorViews = selectorViews;
            _weaponStaticDataService = weaponStaticDataService;
            _wallet = wallet;
            _buyButtonView = buyButtonView;
        }

        public void Initialize()
        {
            _buyButtonView.Clicked += TryRemoveMoney;
            _selectorViews.ForEach(x=> x.Choosed += SetLastChoosedWeapon);
        }

        public void Dispose()
        {
            _buyButtonView.Clicked -= TryRemoveMoney;
            _selectorViews.ForEach(x=> x.Choosed -= SetLastChoosedWeapon);
        }

        private void SetLastChoosedWeapon(WeaponTypeId weaponTypeId) => 
            _weaponId = weaponTypeId;

        private void TryRemoveMoney()
        {
            var cost = _weaponStaticDataService.Get(_weaponId).Price.Value;
            
            if(_wallet.TryRemoveMoney(cost))
                Succeeded?.Invoke(_weaponId);
        }
    }
}