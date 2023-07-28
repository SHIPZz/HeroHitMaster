using System.Collections.Generic;
using Enums;
using Gameplay.Character.Player;
using Gameplay.Character.Players;
using ScriptableObjects.PlayerSettings;

namespace Services.Providers
{
    public class PlayerProvider
    {
        public Player CurrentPlayer { get; set; }
    }
}