using System;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using UnityEngine;

namespace CodeBase.Services.Ad
{
    public class AdInvokerService : IAdInvokerService
    {
        private const int TargetAdInvoke = 3;
        private readonly IAdService _adService;
        private readonly IWorldDataService _worldDataService;
        private readonly IPauseService _pauseService;

        public bool AdEnabled { get; private set; }

        public AdInvokerService(IWorldDataService worldDataService, IAdService adService, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _worldDataService = worldDataService;
            _adService = adService;
        }

        public void Invoke(Action onCloseCallback = null)
        {
            if (_worldDataService.WorldData.LevelData.Id % TargetAdInvoke == 0)
            {
                AdEnabled = true;
                _adService.PlayShortAd(StartCallback, closed => OnEndCallback(onCloseCallback, closed),
                    OnErrorCallback, OnOfflineCallback);
                return;
            }

            AdEnabled = false;
            onCloseCallback?.Invoke();
        }

        private void OnOfflineCallback()
        {
            _pauseService.UnPause();
            AdEnabled = false;
            AudioListener.volume = 1;
        }

        private void OnErrorCallback(string request)
        {
            _pauseService.UnPause();
            AdEnabled = false;
            AudioListener.volume = 1;
        }

        private void OnEndCallback(Action onCloseCallback, bool closed)
        {
            if (!closed)
                return;

            _pauseService.UnPause();
            onCloseCallback?.Invoke();
            AdEnabled = false;
            AudioListener.volume = 1;
        }

        private void StartCallback()
        {
            _pauseService.Pause();
            AudioListener.volume = 0f;
        }
    }
}