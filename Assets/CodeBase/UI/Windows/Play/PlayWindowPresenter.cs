using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.Infrastructure;
using CodeBase.Services.Providers;
using CodeBase.UI.Windows.Popup;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.UI.Windows.Play
{
    public class PlayWindowPresenter : IInitializable, IDisposable, IGameplayRunnable
    {
        private readonly WindowService _windowService;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly PopupInfoView _popupInfoView;

        public PlayWindowPresenter(WindowService windowService, 
            ILoadingCurtain loadingCurtain, 
            PopupInfoView popupInfoView)
        {
            _popupInfoView = popupInfoView;
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
            while (_popupInfoView.isActiveAndEnabled)
                await UniTask.Yield();
                
            _windowService.Open(WindowTypeId.Play);
        }
    }
}