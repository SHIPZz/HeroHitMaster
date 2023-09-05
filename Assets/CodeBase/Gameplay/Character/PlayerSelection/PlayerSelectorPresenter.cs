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
        private WeaponSelector _weaponSelector;

        public PlayerSelectorPresenter(PlayerSelector playerSelector,
            WeaponSelector weaponSelector)
        {
            _weaponSelector = weaponSelector;
            _playerSelector = playerSelector;
        }

        public void Initialize()
        {
            _weaponSelector.NewWeaponChanged += _playerSelector.Select;
        }

        public void Dispose()
        {
            _weaponSelector.NewWeaponChanged -= _playerSelector.Select;
        }
    }
}