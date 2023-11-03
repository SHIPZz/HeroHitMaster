using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.Infrastructure;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.UI.Windows.Play
{
    public class PlayWindowPresenter : IInitializable, IDisposable, IGameplayRunnable
    {
        private const float OpenDelay = 1f;
        
        private readonly WindowService _windowService;
        private readonly ILoadingCurtain _loadingCurtain;

        public PlayWindowPresenter(WindowService windowService, ILoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
            _windowService = windowService;
        }

        public void Initialize() => 
            _loadingCurtain.Closed += OnLoadingCurtainOnClosed;

        public void Dispose() => 
            _loadingCurtain.Closed -= OnLoadingCurtainOnClosed;

        public void Run() => 
            _windowService.Close(WindowTypeId.Play);

        private async void OnLoadingCurtainOnClosed()
        {
            await UniTask.WaitForSeconds(OpenDelay);
            _windowService.Open(WindowTypeId.Play);
        }
    }
}