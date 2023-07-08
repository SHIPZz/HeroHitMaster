using System;
using Enums;
using Services.GameObjectsPoolAccess;
using Zenject;

namespace Gameplay.PlayerSelection
{
    public class PlayerSwitcherHandler : IInitializable, IDisposable
    {
        private readonly PlayerSelector _playerSelector;
        private readonly PlayerStorage _playerStorage;

        public PlayerSwitcherHandler(PlayerSelector playerSelector, PlayerStorage playerStorage)
        {
            _playerSelector = playerSelector;
            _playerStorage = playerStorage;
        }

        public void Initialize()
        {
            _playerSelector.NewPlayerChanged += Enable;
        }

        public void Dispose()
        {
            _playerSelector.NewPlayerChanged -= Enable;
        }

        private void Enable(WeaponTypeId weaponTypeId)
        {
            _playerStorage.Get(weaponTypeId);
        }
    }
}