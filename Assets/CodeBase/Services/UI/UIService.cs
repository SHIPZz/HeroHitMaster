using CodeBase.Enums;
using CodeBase.Infrastructure;
using CodeBase.Services.Ad;
using CodeBase.Services.Pause;
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
        private readonly Canvas _mainUi;
        private readonly ShopWeaponInfoView _shopWeaponInfoView;
        private readonly WalletPresenter _walletPresenter;
        private readonly ShopWeaponPresenter _shopWeaponPresenter;
        private WindowService _windowService;
        private readonly IPauseService _pauseService;
        private readonly PlayWindowPresenter _playWindowPresenter;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IAdInvokerService _adInvokerService;

        public UIService(Canvas mainUi, ShopWeaponInfoView shopWeaponInfoView, WalletPresenter walletPresenter,
            ShopWeaponPresenter shopWeaponPresenter,
            WindowService windowService,
            IPauseService pauseService,
            PlayWindowPresenter playWindowPresenter
            , ILoadingCurtain loadingCurtain, IAdInvokerService adInvokerService)
        {
            _adInvokerService = adInvokerService;
            _loadingCurtain = loadingCurtain;
            _playWindowPresenter = playWindowPresenter;
            _pauseService = pauseService;
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

        public async void EnableMainUI()
        {
            _adInvokerService.Invoke();

            while (_adInvokerService.AdEnabled)
                await UniTask.Yield();

            _pauseService.UnPause();
            _playWindowPresenter.OnLoadingCurtainOnClosed();
            _mainUi.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetUpdate(true).OnComplete(() =>
            {
                DOTween.Sequence().AppendInterval(1.5f).SetUpdate(true).OnComplete(() => _loadingCurtain.Hide());
            });
        }
    }
}