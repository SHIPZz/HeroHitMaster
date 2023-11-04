using System.Collections.Generic;
using CodeBase.Infrastructure;
using CodeBase.Services.Storages.Character;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class PlayerShootInputInitializer : IGameplayRunnable
    {
        private readonly List<Player> _players;

        public PlayerShootInputInitializer(IPlayerStorage playerStorage) => 
            _players = playerStorage.GetAll();

        public void Run() => 
            _players.ForEach(x=>x.GetComponent<PlayerShootInput>().UnBlock());
    }
}