using System;
using System.Collections.Generic;
using Zenject;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponPresenter : IInitializable, IDisposable
    {
        private readonly List<WeaponSelectorView> _weaponSelectorViews;
        private readonly ShopWeaponInfoWriter _shopWeaponInfoWriter;

        public ShopWeaponPresenter(List<WeaponSelectorView> weaponSelectorViews, ShopWeaponInfoWriter shopWeaponInfoWriter)
        {
            _weaponSelectorViews = weaponSelectorViews;
            _shopWeaponInfoWriter = shopWeaponInfoWriter;
        }

        public void Initialize()
        {
            _weaponSelectorViews.ForEach(x => x.Choosed += _shopWeaponInfoWriter.TryWrite);
        }

        public void Dispose()
        {
            _weaponSelectorViews.ForEach(x => x.Choosed -= _shopWeaponInfoWriter.TryWrite);
        }
    }
}