using System.Collections.Generic;
using Enums;
using Gameplay.Character.Player;
using ScriptableObjects.PlayerSettings;

namespace Services.Providers
{
    public class PlayerProvider
    {
        private readonly List<PlayerSettings> _playerSettings;

        public List<PlayerTypeId> AvailableCharacters { get; set; } = new();

        public Dictionary<PlayerTypeId, Player> Players { get; set; } = new();

        public Player CurrentPlayer { get; set; }
    }
}