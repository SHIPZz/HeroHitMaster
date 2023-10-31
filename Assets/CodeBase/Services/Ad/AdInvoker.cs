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

        public void Init()
        {
            if (_worldDataService.WorldData.LevelData.Id % TargetAdInvoke == 0)
                _adService.PlayShortAd(null, null);
        }
    }
}