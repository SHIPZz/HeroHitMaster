using System;
using Enums;
using Zenject;

namespace Services.WeaponSelection
{
    public class WeaponSelectorPresenter : IInitializable, IDisposable
    {
        private readonly WeaponSelectorView _weaponSelectorView;
        private readonly WeaponSelector _weaponSelector;

        public WeaponSelectorPresenter(WeaponSelectorView weaponSelectorView, WeaponSelector weaponSelector)
        {
            _weaponSelectorView = weaponSelectorView;
            _weaponSelector = weaponSelector;
        }
        
        public void Initialize()
        {
            _weaponSelectorView.LeftArrowClicked += _weaponSelector.SelectPreviousWeapon;
            _weaponSelectorView.RightArrowClicked += _weaponSelector.SelectNextWeapon;
            _weaponSelectorView.ApplyButtonClicked += _weaponSelector.SaveCurrentWeapon;
            _weaponSelector.WeaponChanged += OnWeaponChanged;
        }

        public void Dispose()
        {
            _weaponSelectorView.LeftArrowClicked -= _weaponSelector.SelectPreviousWeapon;
            _weaponSelector.WeaponChanged -= OnWeaponChanged;
            _weaponSelectorView.RightArrowClicked -= _weaponSelector.SelectNextWeapon;
            _weaponSelectorView.ApplyButtonClicked -= _weaponSelector.SaveCurrentWeapon;
        }

        private void OnWeaponChanged(WeaponTypeId weaponTypeId)
        {
            _weaponSelectorView.ShowWeaponIcon(weaponTypeId);
        }
    }
}