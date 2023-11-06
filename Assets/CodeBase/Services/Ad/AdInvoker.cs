using System;
using CodeBase.Services.Providers;

namespace CodeBase.Services.Ad
{
    public class AdInvoker : IAdInvoker
    {
        private const int TargetAdInvoke = 3;
        private readonly IAdService _adService;
        private readonly IWorldDataService _worldDataService;

        public AdInvoker(IAdService adService, IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
            _adService = adService;
        }

        public void Init(Action onStartCallback, Action onCloseCallback)
        {
            if (_worldDataService.WorldData.LevelData.Id % TargetAdInvoke == 0)
                _adService.PlayShortAd(() => onStartCallback?.Invoke(), (closed) =>
                {
                    if (closed)
                        onCloseCallback?.Invoke();
                });
        }
    }
}