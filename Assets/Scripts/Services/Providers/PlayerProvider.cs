using System.Collections.Generic;
using Enums;
using Gameplay.Character.Player;
using ScriptableObjects.PlayerSettings;

namespace Services.Providers
{
    public class PlayerProvider
    {
        private readonly List<PlayerSettings> _playerSettings;

        public PlayerProvider(List<PlayerSettings> playerSettings)
        {
            _playerSettings = playerSettings;
            FillDicitonary();
        }
        
        public Dictionary<PlayerTypeId, PlayerSettings> PlayerConfigs { get; set; } = new();

        public List<PlayerTypeId> AvailableCharacters { get; set; } = new();
        
        public Player CurrentPlayer { get; set; }

        private void FillDicitonary()
        {
            foreach (var playerSetting in _playerSettings)
            {
                PlayerConfigs[playerSetting.PlayerTypeId] = playerSetting;
            }
        }
    }
}