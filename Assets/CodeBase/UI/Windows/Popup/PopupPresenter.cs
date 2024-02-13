using System;
using CodeBase.Enums;
using CodeBase.Infrastructure;
using CodeBase.Services.Ad;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using Zenject;

namespace CodeBase.UI.Windows.Popup
{
    public class PopupPresenter : IInitializable, IDisposable
    {
        private const int TargetPopupLevelInvoke = 4;

        private readonly PopupInfoView _popupInfoView;
        private readonly WindowService _windowService;
        private readonly IAdService _adService;
        private readonly IWorldDataService _worldDataService;
        private readonly IPauseService _pauseService;
        private readonly ILoadingCurtain _loadingCurtain;

        public PopupPresenter(WindowService windowService, PopupInfoView popupInfoView,
            IAdService adService,
            IWorldDataService worldDataService, 
            IPauseService pauseService, ILoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
            _pauseService = pauseService;
            _worldDataService = worldDataService;
            _adService = adService;
            _popupInfoView = popupInfoView;
            _windowService = windowService;
        }

        public void Initialize()
        {
            _popupInfoView.AdButtonClicked += ShowAd;

            if (_worldDataService.WorldData.LevelData.Id % TargetPopupLevelInvoke != 0 || _worldDataService.WorldData.LevelData.Id % 3 == 0)
                return;
            
            _loadingCurtain.Closed += OnLoadingCurtainClosed;
        }

        public void Dispose()
        {
            _popupInfoView.AdButtonClicked -= ShowAd;
            _loadingCurtain.Closed -= OnLoadingCurtainClosed;
        }

        private void OnLoadingCurtainClosed()
        {
            _pauseService.Pause();
            _windowService.Close(WindowTypeId.Play);
            _windowService.OpenQuickly(WindowTypeId.Popup);
        }

        private void ShowAd()
        {
            _pauseService.Pause();
            
            _adService.PlayLongAd(null, EndCallback, OnErrorCallback);
        }

        private void OnErrorCallback(string error)
        {
            OpenPlayWindow();
        }

        private async void EndCallback()
        {
            await _popupInfoView.StartChooseRandomWeapon();
            OpenPlayWindow();
        }

        private void OpenPlayWindow()
        {
            _windowService.CloseAll();
            _pauseService.UnPause();
            _windowService.Open(WindowTypeId.Play);
        }
    }
}