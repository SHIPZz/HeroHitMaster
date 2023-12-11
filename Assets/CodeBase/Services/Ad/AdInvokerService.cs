using System;
using CodeBase.Infrastructure;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Ad
{
    public class AdInvokerService : IAdInvokerService
    {
        private const int TargetAdInvoke = 3;
        private readonly IAdService _adService;
        private readonly IWorldDataService _worldDataService;
        private readonly IPauseService _pauseService;

        public AdInvokerService(IWorldDataService worldDataService, IAdService adService, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _worldDataService = worldDataService;
            _adService = adService;
        }
        
        public void Invoke(Action onCloseCallback)
        {
            if (_worldDataService.WorldData.LevelData.Id % TargetAdInvoke == 0)
            {
                _adService.PlayShortAd(StartCallback, closed => OnEndCallback(onCloseCallback, closed));
            }
        }

        private void OnEndCallback(Action onCloseCallback, bool closed)
        {
            if (!closed)
                return;
            
            _pauseService.UnPause();
            onCloseCallback?.Invoke();
            AudioListener.volume = 1;
        }

        private void StartCallback()
        {
            _pauseService.Pause();
            AudioListener.volume = 0f;
        }
    }
}