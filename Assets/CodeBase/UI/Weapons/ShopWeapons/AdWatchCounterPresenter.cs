using System;
using Zenject;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class AdWatchCounterPresenter : IInitializable, IDisposable
    {
        private readonly ShopWeaponInfoView _shopWeaponInfoView;
        private readonly AdWatchCounter _adWatchCounter;

        public AdWatchCounterPresenter(ShopWeaponInfoView shopWeaponInfoView, AdWatchCounter adWatchCounter)
        {
            _shopWeaponInfoView = shopWeaponInfoView;
            _adWatchCounter = adWatchCounter;
        }

        public void Initialize() => 
            _shopWeaponInfoView.AdButtonClicked += _adWatchCounter.Count;

        public void Dispose() => 
            _shopWeaponInfoView.AdButtonClicked -= _adWatchCounter.Count;
    }
}