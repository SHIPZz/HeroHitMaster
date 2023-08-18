using System;
using System.Collections.Generic;
using CodeBase.UI.Weapons;
using Zenject;

namespace CodeBase.Services.SaveSystems.SaveTriggers
{
    public class PurchasedWeaponsTriggerPresenter : IInitializable, IDisposable
    {
        private List<WeaponSelectorView> _weaponSelectorViews;
        private PurchasedWeaponsSaveTrigger _purchasedWeaponsSaveTrigger;

        public PurchasedWeaponsTriggerPresenter(List<WeaponSelectorView> weaponSelectorViews, 
            PurchasedWeaponsSaveTrigger purchasedWeaponsSaveTrigger)
        {
            _weaponSelectorViews = weaponSelectorViews;
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