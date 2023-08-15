using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.UI.Weapons;
using Zenject;

namespace CodeBase.UI.Windows.Buy
{
    public class BuyWindowPresenter : IInitializable, IDisposable
    {
        private readonly Dictionary<WeaponTypeId, WeaponSelectorView> _weaponIcons;
        private readonly WindowService _windowService;

        public BuyWindowPresenter(IProvider<Dictionary<WeaponTypeId, WeaponSelectorView>> weaponIconsProvider,
            WindowService windowService)
        {
            _windowService = windowService;
            _weaponIcons = weaponIconsProvider.Get();
        }

        public void Initialize()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIcons.Values)
                weaponIcon.Choosed += OnWeaponIconChoosed;
        }

        public void Dispose()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIcons.Values)
                weaponIcon.Choosed -= OnWeaponIconChoosed;
        }

        private void OnWeaponIconChoosed(WeaponTypeId weaponTypeId) => 
            _windowService.Open(WindowTypeId.Buy);
    }
}