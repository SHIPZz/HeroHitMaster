using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.Storages.Character;

namespace CodeBase.Gameplay.Character.PlayerSelection
{
    public class PlayerSelector
    {
        private readonly IPlayerStorage _playerStorage;
        private readonly IWorldDataService _worldDataService;

        public PlayerSelector(IPlayerStorage playerStorage, IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
            _playerStorage = playerStorage;
        }

        public void Select(WeaponTypeId weaponTypeId)
        {
            Player lastPlayer = _playerStorage.CurrentPlayer;
            
            if (lastPlayer == _playerStorage.GetByWeapon(weaponTypeId))
            {
                SetPlayerToData(_worldDataService.WorldData.PlayerData, lastPlayer);
                return;
            }

            Player targetPlayer = _playerStorage.GetByWeapon(weaponTypeId);
            SetPlayerToData(_worldDataService.WorldData.PlayerData, targetPlayer);
        }

        private void SetPlayerToData(PlayerData playerData, Player targetPlayer)
        {
            playerData.LastPlayerId = targetPlayer.PlayerTypeId;
        }
    }
}