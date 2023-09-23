using System;
using CodeBase.Enums;
using CodeBase.Services.Ad;
using CodeBase.Services.Pause;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows.Popup
{
    public class PopupPresenter : IInitializable, IDisposable
    {
         private readonly PopupInfoView _popupInfoView;
        
        private readonly WindowService _windowService;
        private readonly IAdService _adService;
        private IPauseService _pauseService;

        public PopupPresenter(WindowService windowService, PopupInfoView popupInfoView, IAdService adService, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _adService = adService;
            _popupInfoView = popupInfoView;
            _windowService = windowService;
        }

        public void Initialize()
        {
            _popupInfoView.AdButtonClicked += ShowAd;
        }

        public void Dispose()
        {
            _popupInfoView.AdButtonClicked -= ShowAd;
        }

        private void ShowAd()
        {
            // _adService.PlayLongAd();
        }
    }
}