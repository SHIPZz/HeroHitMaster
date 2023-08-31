using System;
using CodeBase.Enums;
using CodeBase.Services.Ad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows.Popup
{
    public class PopupPresenter : IInitializable, IDisposable
    {
         private PopupInfoView _popupInfoView;
        
        private readonly WindowService _windowService;
        private IAdService _adService;

        public PopupPresenter(WindowService windowService, PopupInfoView popupInfoView, IAdService adService)
        {
            _adService = adService;
            _popupInfoView = popupInfoView;
            _windowService = windowService;
        }

        public void Init()
        {
            _windowService.CloseAll();
            _windowService.Open(WindowTypeId.Popup);
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
            // _popupInfoView.Show();
        }
    }
}