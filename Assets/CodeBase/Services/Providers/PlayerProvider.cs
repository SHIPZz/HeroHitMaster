using CodeBase.Gameplay.Character.Players;

namespace CodeBase.Services.Providers
{
    public class PlayerProvider : IProvider<Player>
    {
        private Player _player;
        
        public Player Get() => 
            _player;

        public void Set(Player player) => 
            _player = player;
    }
}