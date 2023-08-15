using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.UI.Weapons;
using Zenject;

namespace CodeBase.Gameplay.Character.PlayerSelection
{
    public class PlayerSelectorPresenter : IInitializable, IDisposable
    {
        private readonly PlayerSelector _playerSelector;
        private readonly Dictionary<WeaponTypeId, WeaponSelectorView> _weaponSelectorViews;

        public PlayerSelectorPresenter(PlayerSelector playerSelector,
            IProvider<Dictionary<WeaponTypeId, WeaponSelectorView>> weaponSelectorViewsProvider)
        {
            _playerSelector = playerSelector;
            _weaponSelectorViews = weaponSelectorViewsProvider.Get();
        }

        public void Initialize()
        {
            foreach (var weaponSelectorView in _weaponSelectorViews.Values)
            {
                weaponSelectorView.Choosed += _playerSelector.Select;
            }
        }

        public void Dispose()
        {
            foreach (var weaponSelectorView in _weaponSelectorViews.Values)
            {
                weaponSelectorView.Choosed -= _playerSelector.Select;
            }
        }
    }
}