using System;
using CodeBase.Enums;
using CodeBase.Services.Ad;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using Zenject;

namespace CodeBase.UI.Windows.Popup
{
    public class PopupPresenter : IInitializable, IDisposable
    {
        private const int TargetPopupLevelInvoke = 5;

        private readonly PopupInfoView _popupInfoView;
        private readonly WindowService _windowService;
        private readonly IAdService _adService;
        private readonly ISaveSystem _saveSystem;

        public PopupPresenter(WindowService windowService, PopupInfoView popupInfoView,
            IAdService adService, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _adService = adService;
            _popupInfoView = popupInfoView;
            _windowService = windowService;
        }

        public async void Initialize()
        {
            _popupInfoView.AdButtonClicked += ShowAd;

            var levelData = await _saveSystem.Load<LevelData>();

            if (levelData.Id % TargetPopupLevelInvoke != 0)
                return;
            
            _windowService.CloseAll();
            _windowService.Open(WindowTypeId.Popup);
        }

        public void Dispose() => 
            _popupInfoView.AdButtonClicked -= ShowAd;

        private void ShowAd() => 
            _adService.PlayLongAd(null, EndCallback);

        private async void EndCallback()
        {
            await _popupInfoView.StartChooseRandomWeapon();
            _windowService.CloseAll();
            _windowService.Open(WindowTypeId.Play);
        }
    }
}