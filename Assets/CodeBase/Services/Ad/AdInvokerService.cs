using System;
using CodeBase.Infrastructure;
using CodeBase.Services.Providers;
using Zenject;

namespace CodeBase.Services.Ad
{
    public class AdInvokerService : IAdInvokerService
    {
        private const int TargetAdInvoke = 3;
        private readonly IAdService _adService;
        private readonly IWorldDataService _worldDataService;

        public AdInvokerService(IWorldDataService worldDataService, IAdService adService)
        {
            _worldDataService = worldDataService;
            _adService = adService;
        }
        
        public void Invoke(Action onCloseCallback)
        {
            if (_worldDataService.WorldData.LevelData.Id % TargetAdInvoke == 0)
            {
                _adService.PlayShortAd(null, closed =>
                {
                    if(closed)
                        onCloseCallback?.Invoke();
                });
            }
        }

    }
}