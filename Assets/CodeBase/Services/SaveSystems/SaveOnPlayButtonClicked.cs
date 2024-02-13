using CodeBase.Infrastructure;
using CodeBase.Services.Providers;

namespace CodeBase.Services.SaveSystems
{
    public class SaveOnPlayButtonClicked : IGameplayRunnable
    {
        private readonly IWorldDataService _worldDataService;

        public SaveOnPlayButtonClicked(IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
        }

        public void Run()
        {
            _worldDataService.Save();
        }
    }
}