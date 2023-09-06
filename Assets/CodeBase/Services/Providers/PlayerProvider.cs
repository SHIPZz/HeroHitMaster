using System;
using CodeBase.Gameplay.Character.Players;
using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class PlayerProvider : IProvider<Player>, IProvider<PlayerProvider>
    {
        private Player _player;

        public event Action<Player> Changed; 

        public Player Get() => 
            _player;

        public void Set(PlayerProvider t)
        {
        }

        public void Set(Player player)
        {
            _player = player;
            Changed?.Invoke(_player);
        }

        PlayerProvider IProvider<PlayerProvider>.Get() => 
        this;
    }
}