using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.Storages.Character;
using UnityEngine;

namespace CodeBase.Gameplay.Character.PlayerSelection
{
    public class PlayerSelector
    {
        private readonly IPlayerStorage _playerStorage;
        private readonly ISaveSystem _saveSystem;

        public PlayerSelector(IPlayerStorage playerStorage, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _playerStorage = playerStorage;
        }

        public async void Select(WeaponTypeId weaponTypeId)
        {
            Player lastPlayer = _playerStorage.CurrentPlayer;
            var worldData = await _saveSystem.Load<WorldData>();
            
            if (lastPlayer == _playerStorage.GetByWeapon(weaponTypeId))
            {
                SetPlayerToData(worldData.PlayerData, lastPlayer);
                return;
            }

            Player targetPlayer = _playerStorage.GetByWeapon(weaponTypeId);
            SetPlayerToData(worldData.PlayerData, targetPlayer);
        }

        private void SetPlayerToData(PlayerData playerData, Player targetPlayer)
        {
            playerData.LastPlayerId = targetPlayer.PlayerTypeId;
        }
    }
}