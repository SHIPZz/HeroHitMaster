using System;
using Enums;
using Gameplay.Character.Player;
using Services.Providers;
using Services.Storages;

namespace Gameplay.PlayerSelection
{
    public class PlayerSelector
    {
        private readonly IPlayerStorage _playerStorage;

        public event Action<WeaponTypeId> NewPlayerChanged;

        public PlayerSelector(IPlayerStorage playerStorage)
        {
            _playerStorage = playerStorage;
        }

        public void SelectByWeaponType(WeaponTypeId weaponTypeId)
        {
            _playerStorage.Get(weaponTypeId);
            NewPlayerChanged?.Invoke(weaponTypeId);
        }
    }
}