using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Providers;

namespace CodeBase.Services.GameContinues
{
    public class GameContinue
    {
        private readonly PlayerProvider _playerProvider;

        public GameContinue(IProvider<PlayerProvider> playerProvider)
        {
            _playerProvider = playerProvider.Get();
        }

        public void Continue()
        {
            var playerHealth = _playerProvider.Get().GetComponent<PlayerHealth>();
            playerHealth.Heal(100);
            playerHealth.gameObject.SetActive(true);
        }
    }
}