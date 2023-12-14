using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.MusicHandlerSystem;
using CodeBase.Services.Ad;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using UnityEngine;

namespace CodeBase.Services.GameContinues
{
    public class AdReward
    {
        private readonly PlayerProvider _playerProvider;
        private readonly IAdService _adService;
        private readonly MusicHandler _musicHandler;
        private readonly IPauseService _pauseService;

        public AdReward(IProvider<PlayerProvider> playerProvider, 
            IAdService adService, 
            MusicHandler musicHandler,
            IPauseService pauseService)
        {
            _pauseService = pauseService;
            _musicHandler = musicHandler;
            _adService = adService;
            _playerProvider = playerProvider.Get();
        }

        public void Do() => 
            _adService.PlayLongAd(null, RecoverPlayerOnEndAd);

        private void RecoverPlayerOnEndAd()
        {
            var playerHealth = _playerProvider.Get().GetComponent<PlayerHealth>();
            playerHealth.Heal(100);
            AudioListener.volume = 1f;
            AudioListener.pause = false;
            _pauseService.UnPause();
            _musicHandler.Play();
            playerHealth.gameObject.SetActive(true);
        }
    }
}