using System;
using CodeBase.UI.Windows.Shop;
using Zenject;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class AdWatchCounterPresenter : IInitializable, IDisposable
    {
        private readonly ShopWeaponInfoView _shopWeaponInfoView;
        private readonly WeaponAdWatchCounter _weaponAdWatchCounter;

        public AdWatchCounterPresenter(ShopWeaponInfoView shopWeaponInfoView, WeaponAdWatchCounter weaponAdWatchCounter)
        {
            _shopWeaponInfoView = shopWeaponInfoView;
            _weaponAdWatchCounter = weaponAdWatchCounter;
        }

        public void Initialize() => 
            _shopWeaponInfoView.AdButtonClicked += _weaponAdWatchCounter.Count;

        public void Dispose() => 
            _shopWeaponInfoView.AdButtonClicked -= _weaponAdWatchCounter.Count;
    }
}