using System;
using System.Collections.Generic;
using Enums;
using UI;
using Zenject;

namespace Gameplay.PlayerSelection
{
    public class PlayerSelectorPresenter : IInitializable, IDisposable
    {
        private readonly PlayerSelectorView _playerSelectorView;
        private readonly PlayerSelector _playerSelector;
        private readonly Dictionary<WeaponTypeId, WeaponSelectorView> _icons;

        public PlayerSelectorPresenter(WeaponIconsProvider weaponIconsProvider,PlayerSelector playerSelector)
        {
            _icons = weaponIconsProvider.Icons;
            
            _playerSelector = playerSelector;
        }

        public void Initialize()
        {
            foreach (var weaponSelectorView in _icons.Values)
            {
                weaponSelectorView.Choosed += _playerSelector.SelectByWeaponType;
            }
        }

        public void Dispose()
        {
            foreach (var weaponSelectorView in _icons.Values)
            {
                weaponSelectorView.Choosed -= _playerSelector.SelectByWeaponType;
            }
        }
    }
}