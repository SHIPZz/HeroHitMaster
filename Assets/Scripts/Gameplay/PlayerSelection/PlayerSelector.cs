using System;
using Enums;
using Gameplay.Character.Player;
using Services.GameObjectsPoolAccess;
using Services.Providers;

namespace Gameplay.PlayerSelection
{
    public class PlayerSelector
    {
        private readonly PlayerProvider _playerProvider;
        private readonly PlayerStorage _playerStorage;
        private PlayerTypeId _playerTypeId;
        private  int _currentPlayerId;

        public event Action<Player> OldPlayerChanged;
        public event Action<WeaponTypeId> NewPlayerChanged;

        public PlayerSelector(PlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
        }

        public void SelectByWeaponType(WeaponTypeId weaponTypeId)
        { 
            OldPlayerChanged?.Invoke(_playerProvider.CurrentPlayer);
            NewPlayerChanged?.Invoke(weaponTypeId);
        }
    }
}