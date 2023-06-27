using System.Collections.Generic;
using Databases;
using Gameplay.Web;
using Services.Providers;
using Services.Providers.AssetProviders;
using UnityEngine;

namespace Services.Inputs.Weapon
{
    public class WeaponSelectorHandler
    {
        private AssetProvider _assetProvider;
        private GameObject _webPrefab;
        private GameObjectPoolProvider _gameObjectPoolProvider;
        private readonly WeaponSelectorInput _weaponSelectorInput;
        private readonly WeaponsProvider _weaponsProvider;
        private IReadOnlyDictionary<int, IWeapon> _weapons;
        private int _currentWeaponId;
        private IWeapon _currentWeapon;
        
        //test
        public WeaponSelectorHandler(AssetProvider assetProvider, 
            GameObjectPoolProvider gameObjectPoolProvider,
            WeaponSelectorInput weaponSelectorInput, WeaponsProvider weaponsProvider)
        {
            _gameObjectPoolProvider = gameObjectPoolProvider;
            _weaponSelectorInput = weaponSelectorInput;
            _weaponsProvider = weaponsProvider;
            _weapons = _weaponsProvider.Weapons;
            _weaponSelectorInput.LeftArrowClicked += OnLeftArrowClicked;
            _weaponSelectorInput.RightArrowClicked += OnRightArrowClicked;
            _weaponSelectorInput.ApplyButtonClicked += OnApllyButtonClicked;
        }

        private void OnApllyButtonClicked()
        {
            //save selected weapon
        }

        private void OnRightArrowClicked()
        {
            _currentWeaponId++;

            if (_currentWeaponId >= _weapons.Count)
                _currentWeaponId = 0;

            _currentWeapon.GameObject.SetActive(false);
            
            SetActiveWeapon();
        }
        
        private void SetActiveWeapon()
        {
            // OldWeaponSwitched?.Invoke(_currentWeapon);
            
            _currentWeapon = _weapons[_currentWeaponId];
            _currentWeapon.GameObject.SetActive(false);
            
            // ChoosedWeapon?.Invoke(_currentWeapon);
        }

        private void OnLeftArrowClicked()
        {
            _currentWeaponId--;

            if (_currentWeaponId < 0)
                _currentWeaponId = _weapons.Count - 1;
            
            _currentWeapon.GameObject.SetActive(false);
            
            SetActiveWeapon();
        }
    }
}