using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using Zenject;

namespace CodeBase.UI.Weapons
{
    class WeaponSelectorPresenter : IInitializable, IDisposable
    {
        private readonly WeaponSelector _weaponSelector;
        private readonly Dictionary<WeaponTypeId, WeaponSelectorView> _weaponIconClickers;
        private readonly ISaveSystem _saveSystem;

        public WeaponSelectorPresenter(WeaponIconsProvider weaponIconsProvider, WeaponSelector weaponSelector,
            ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _weaponIconClickers = weaponIconsProvider.Icons;
            _weaponSelector = weaponSelector;
        }

        public void Initialize()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosed += OnWeaponIconChoosed;
        }

        public void Dispose()
        {
            foreach (WeaponSelectorView weaponIcon in _weaponIconClickers.Values)
                weaponIcon.Choosed -= OnWeaponIconChoosed;
        }

        private async void OnWeaponIconChoosed(WeaponTypeId weaponTypeId)
        {
            var playerData = await _saveSystem.Load<PlayerData>();
            
            if(playerData.PurchasedWeapons.Contains(weaponTypeId))
                return;

            _weaponSelector.Select(weaponTypeId);
        }
    }
}