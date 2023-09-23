using System;
using Zenject;

namespace CodeBase.UI.Windows.Buy
{
    public class BuyWeaponPresenter : IInitializable, IDisposable
    {
        private readonly BuyButtonView _buyButtonView;
        private readonly WeaponBuyer _weaponBuyer;

        public BuyWeaponPresenter(BuyButtonView buyButtonView, WeaponBuyer weaponBuyer)
        {
            _weaponBuyer = weaponBuyer;
            _buyButtonView = buyButtonView;
        }

        public void Initialize() =>
            _buyButtonView.Clicked += _weaponBuyer.Buy;

        public void Dispose() =>
            _buyButtonView.Clicked -= _weaponBuyer.Buy;
    }
}