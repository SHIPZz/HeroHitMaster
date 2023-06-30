using System;
using Zenject;

namespace Gameplay.WeaponSelection
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
            _weaponSelectorView.LeftArrowClicked += _weaponSelector.SelectPrevious;
            _weaponSelectorView.RightArrowClicked += _weaponSelector.SelectNext;
            _weaponSelectorView.ApplyButtonClicked += _weaponSelector.SaveCurrent;
            _weaponSelector.WeaponChanged += _weaponSelectorView.ShowWeaponIcon;
        }

        public void Dispose()
        {
            _weaponSelectorView.LeftArrowClicked -= _weaponSelector.SelectPrevious;
            _weaponSelector.WeaponChanged -=  _weaponSelectorView.ShowWeaponIcon;
            _weaponSelectorView.RightArrowClicked -= _weaponSelector.SelectNext;
            _weaponSelectorView.ApplyButtonClicked -= _weaponSelector.SaveCurrent;
        }
    }
}