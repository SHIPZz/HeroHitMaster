using System;
using Databases;
using Services.Factories;
using Zenject;

namespace Services.WeaponSelection
{
    public class WeaponSelectorPresenter : IInitializable, IDisposable
    {
        private readonly WeaponSelectorView _weaponSelectorView;
        private readonly WeaponSelector _weaponSelector;
        private readonly GameFactory _gameFactory;
        private readonly WeaponsProvider _weaponsProvider;

        public WeaponSelectorPresenter(WeaponSelectorView weaponSelectorView, WeaponSelector weaponSelector,
            GameFactory gameFactory, WeaponsProvider weaponsProvider)
        {
            _weaponSelectorView = weaponSelectorView;
            _weaponSelector = weaponSelector;
            _gameFactory = gameFactory;
            _weaponsProvider = weaponsProvider;
        }
        
        public void Initialize()
        {
            _weaponSelectorView.LeftArrowClicked += _weaponSelector.SelectPreviousWeapon;
            _weaponSelectorView.RightArrowClicked += _weaponSelector.SelectNextWeapon;
            _weaponSelectorView.ApplyButtonClicked += Create;
        }

        public void Dispose()
        {
            _weaponSelectorView.LeftArrowClicked -= _weaponSelector.SelectPreviousWeapon;
            _weaponSelectorView.RightArrowClicked -= _weaponSelector.SelectNextWeapon;
            _weaponSelectorView.ApplyButtonClicked -= Create;
        }

        private void Create() => 
            _weaponsProvider.CurrentWeapon = _gameFactory.CreateWeapon();
    }
}