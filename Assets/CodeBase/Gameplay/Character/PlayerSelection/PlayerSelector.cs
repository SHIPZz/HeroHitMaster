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
            var playerData = await _saveSystem.Load<PlayerData>();
            
            if (lastPlayer == _playerStorage.GetByWeapon(weaponTypeId))
            {
                SavePlayer(playerData, lastPlayer);
                return;
            }

            Player targetPlayer = _playerStorage.GetByWeapon(weaponTypeId);
            SavePlayer(playerData, targetPlayer);
        }

        private void SavePlayer(PlayerData playerData, Player targetPlayer)
        {
            playerData.LastPlayerId = targetPlayer.PlayerTypeId;
            _saveSystem.Save(playerData);
        }
    }
}