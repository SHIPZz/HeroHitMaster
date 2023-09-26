using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Ad;
using CodeBase.Services.Providers;

namespace CodeBase.Services.GameContinues
{
    public class AdReward
    {
        private readonly PlayerProvider _playerProvider;
        private readonly IAdService _adService;

        public AdReward(IProvider<PlayerProvider> playerProvider, IAdService adService)
        {
            _adService = adService;
            _playerProvider = playerProvider.Get();
        }

        public void Do()
        {
            _adService.PlayLongAd(null, RecoverPlayerOnEndAd);
        }

        private void RecoverPlayerOnEndAd()
        {
            var playerHealth = _playerProvider.Get().GetComponent<PlayerHealth>();
            playerHealth.Heal(100);
            playerHealth.gameObject.SetActive(true);
        }
    }
}