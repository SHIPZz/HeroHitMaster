using System;
using System.Collections.Generic;
using CodeBase.Services.Providers;
using CodeBase.UI.Weapons;
using Zenject;

namespace CodeBase.Services.SaveSystems.SaveTriggers
{
    public class PurchasedWeaponsTriggerPresenter : IInitializable, IDisposable
    {
        private readonly List<WeaponSelectorView> _weaponSelectorViews;
        private readonly PurchasedWeaponsSaveTrigger _purchasedWeaponsSaveTrigger;

        public PurchasedWeaponsTriggerPresenter(IProvider<List<WeaponSelectorView>> weaponSelectorViewsProvider, 
            PurchasedWeaponsSaveTrigger purchasedWeaponsSaveTrigger)
        {
            _weaponSelectorViews = weaponSelectorViewsProvider.Get();
            _purchasedWeaponsSaveTrigger = purchasedWeaponsSaveTrigger;
        }

        public void Initialize()
        {
            _weaponSelectorViews.ForEach(x => x.Choosed += _purchasedWeaponsSaveTrigger.SetLastWeaponType);
        }

        public void Dispose()
        {
            _weaponSelectorViews.ForEach(x => x.Choosed -= _purchasedWeaponsSaveTrigger.SetLastWeaponType);
        }
    }
}