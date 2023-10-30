using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;

namespace CodeBase.Services.Ad
{
    public class AdInvoker : IAdInvoker
    {
        private const int TargetAdInvoke = 3;

        private readonly IAdService _adService;
        private readonly ISaveSystem _saveSystem;

        public AdInvoker(IAdService adService, ISaveSystem saveSystem)
        {
            _adService = adService;
            _saveSystem = saveSystem;
        }

        public async void Init()
        {
            var levelData = await _saveSystem.Load<LevelData>();
            
            if (levelData.Id % TargetAdInvoke == 0)
                _adService.PlayShortAd(null, null);
        }
    }
}