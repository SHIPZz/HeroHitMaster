using CodeBase.Enums;
using CodeBase.Infrastructure;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.UI.Wallet;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Play;
using CodeBase.UI.Windows.Shop;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Services.UI
{
    public class UIService
    {
        private const int TargetPopupLevelInvoke = 4;
        private readonly Canvas _mainUi;
        private readonly ShopWeaponInfoView _shopWeaponInfoView;
        private readonly WalletPresenter _walletPresenter;
        private readonly ShopWeaponPresenter _shopWeaponPresenter;
        private readonly WindowService _windowService;
        private readonly PlayWindowPresenter _playWindowPresenter;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IWorldDataService _worldDataService;
        private bool _isBlocked;

        public UIService(Canvas mainUi, ShopWeaponInfoView shopWeaponInfoView, WalletPresenter walletPresenter,
            ShopWeaponPresenter shopWeaponPresenter,
            WindowService windowService,
            PlayWindowPresenter playWindowPresenter
            , ILoadingCurtain loadingCurtain,
            IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
            _loadingCurtain = loadingCurtain;
            _playWindowPresenter = playWindowPresenter;
            _windowService = windowService;
            _mainUi = mainUi;
            _shopWeaponInfoView = shopWeaponInfoView;
            _walletPresenter = walletPresenter;
            _shopWeaponPresenter = shopWeaponPresenter;
        }

        public void Init(WorldData worldData)
        {
            _mainUi.transform.SetParent(null);
            _shopWeaponInfoView.SetTranslatedNames(worldData.TranslatedWeaponNameData.Names);
            _walletPresenter.Init(worldData.PlayerData.Money);
            _shopWeaponPresenter.Init(worldData.PlayerData.LastNotPopupWeaponId);
        }

        public void EnableMainUI()
        {
            _loadingCurtain.Hide(() =>
            {
                _mainUi.GetComponent<CanvasGroup>().DOFade(1f, 0.3f).SetUpdate(true).OnComplete(HideCurtain);
            });
        }

        private void HideCurtain() =>
            _loadingCurtain.Hide(OnCurtainHidden);

        private async void OnCurtainHidden()
        {
            var currentLevelId = _worldDataService.WorldData.LevelData.Id;

            while (_isBlocked)
                await UniTask.Yield();

            if (currentLevelId % TargetPopupLevelInvoke == 0 && currentLevelId % 3 != 0)
                _windowService.Open(WindowTypeId.Popup);

            _playWindowPresenter.OnLoadingCurtainOnClosed();
        }

        public void BlockEnablingMainUI() =>
            _isBlocked = true;

        public void UnBlock() =>
            _isBlocked = false;
    }
}