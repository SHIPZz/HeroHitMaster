using System;
using CodeBase.Enums;
using CodeBase.Services.Ad;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Popup
{
    public class PopupPresenter : IInitializable, IDisposable
    {
        private const int TargetPopupLevelInvoke = 5;

        private readonly PopupInfoView _popupInfoView;
        private readonly WindowService _windowService;
        private readonly IAdService _adService;
        private readonly IWorldDataService _worldDataService;
        private readonly IPauseService _pauseService;

        public PopupPresenter(WindowService windowService, PopupInfoView popupInfoView,
            IAdService adService,
            IWorldDataService worldDataService, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _worldDataService = worldDataService;
            _adService = adService;
            _popupInfoView = popupInfoView;
            _windowService = windowService;
        }

        public void Initialize()
        {
            _popupInfoView.AdButtonClicked += ShowAd;

            if (_worldDataService.WorldData.LevelData.Id % TargetPopupLevelInvoke != 0)
                return;
            
            _pauseService.Pause();
            _windowService.Close(WindowTypeId.Play);
            _windowService.Open(WindowTypeId.Popup);
        }

        public void Dispose() => 
            _popupInfoView.AdButtonClicked -= ShowAd;

        private void ShowAd()
        {
            _adService.PlayLongAd(null, EndCallback);
        }

        private async void EndCallback()
        {
            await _popupInfoView.StartChooseRandomWeapon();
            _windowService.CloseAll();
            _windowService.Open(WindowTypeId.Play);
        }
    }
}