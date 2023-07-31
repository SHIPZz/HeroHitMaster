using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Character.Players.Shoot;

namespace CodeBase.Services.Providers
{
    public class PlayerProvider
    {
        public Player CurrentPlayer { get; set; }
        public PlayerHealth PlayerHealth => CurrentPlayer.GetComponent<PlayerHealth>();

        public ShootingOnAnimationEvent ShootingOnAnimationEvent =>
            CurrentPlayer.GetComponent<ShootingOnAnimationEvent>();
    }
}