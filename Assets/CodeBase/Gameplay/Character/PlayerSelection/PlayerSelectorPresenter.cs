using System;
using System.Collections.Generic;
using Weapons;
using Zenject;

namespace CodeBase.Gameplay.Character.PlayerSelection
{
    public class PlayerSelectorPresenter : IInitializable, IDisposable
    {
        private readonly PlayerSelector _playerSelector;
        private readonly List<WeaponSelectorView> _weaponSelectorViews;

        public PlayerSelectorPresenter(PlayerSelector playerSelector, List<WeaponSelectorView> weaponSelectorViews)
        {
            _playerSelector = playerSelector;
            _weaponSelectorViews = weaponSelectorViews;
        }

        public void Initialize()
        {
            foreach (var weaponSelectorView in _weaponSelectorViews)
            {
                weaponSelectorView.Choosed += _playerSelector.Select;
            }
        }

        public void Dispose()
        {
            foreach (var weaponSelectorView in _weaponSelectorViews)
            {
                weaponSelectorView.Choosed -= _playerSelector.Select;
            }
        }
    }
}