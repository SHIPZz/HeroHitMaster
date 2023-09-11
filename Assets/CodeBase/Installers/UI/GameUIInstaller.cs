using CodeBase.Gameplay.Character.PlayerSelection;
using CodeBase.Gameplay.LoadNextLevel;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.UI.LevelSlider;
using CodeBase.UI.ShopScrollRects;
using CodeBase.UI.ShopScrollRects.ShopScrollUnderlines;
using CodeBase.UI.Wallet;
using CodeBase.UI.Weapons;
using CodeBase.UI.Weapons.ShopWeapons;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Audio;
using CodeBase.UI.Windows.Buy;
using CodeBase.UI.Windows.Death;
using CodeBase.UI.Windows.Play;
using CodeBase.UI.Windows.Popup;
using CodeBase.UI.Windows.Setting;
using CodeBase.UI.Windows.Shop;
using CodeBase.UI.Windows.Victory;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.UI
{
    public class GameUIInstaller : MonoInstaller
    {
        [SerializeField] private WindowProvider _windowProvider;
        [SerializeField] private WeaponIconsProvider _weaponIconsProvider;
        [SerializeField] private AudioView _audioView;
        [SerializeField] private ShopView _shopView;
        [SerializeField] private SettingView _settingView;
        [SerializeField] private DeathView _deathView;
        [SerializeField] private WalletUI _walletUI;
        [SerializeField] private ScrollRectProvider _scrollRectProvider;
        [SerializeField] private ScrollImagesProvider _scrollImageProvider;
        [SerializeField] private ScrollNameUnderlinesProvider _scrollNameUnderlinesProvider;
        [SerializeField] private ShopWeaponInfoView shopWeaponInfoView;
        [SerializeField] private ShopMoneyText _shopMoneyText;
        [SerializeField] private BuyButtonView _buyButtonView;
        [SerializeField] private WeaponSelectorViewsProvider _weaponSelectorViewsProvider;
        [SerializeField] private Canvas _mainUI;
        [SerializeField] private VictoryInfoView victoryInfoView;
        [SerializeField] private LevelSliderView _levelSliderView;
        [SerializeField] private PopupInfoView _popupInfoView;
        [SerializeField] private AdBuyButtonView _adBuyButtonView;
        [SerializeField] private ContinueButtonView _continueButtonView;
        
        public override void InstallBindings()
        {
            BindWindowProvider();
            BindWindowService();
            BindUIFactory();
            BindWeaponIconsProvider();
            BindShopUI();
            BindPlayerSelector();
            BindAudioUI();
            BindSettingUi();
            BindDeathUI();
            BindPlayUI();
            BindWallet();
            BindScrollUI();
            BindShopWeaponUI();
            BindBuyWeaponUI();
            BindWeaponSelectorViewsProvider();
            BindMainUI();
            BindVictoryUI();
            BindLevelSliderUI();
            BindPopupUI();
            BindAdBuyWeaponButtonView();
            BindContinueButtonView();
            BindLoadNextLevelPresenter();
        }

        private void BindLoadNextLevelPresenter() =>
            Container
                .BindInterfacesAndSelfTo<LoadNextLevelPresenter>()
                .AsSingle();

        private void BindContinueButtonView() => 
            Container
                .BindInstance(_continueButtonView);

        private void BindAdBuyWeaponButtonView() => 
            Container
            .BindInstance(_adBuyButtonView);

        private void BindPopupUI()
        {
            Container.BindInstance(_popupInfoView);
            Container.BindInterfacesAndSelfTo<PopupPresenter>().AsSingle();
        }

        private void BindLevelSliderUI()
        {
            Container.BindInstance(_levelSliderView);
            Container.BindInterfacesAndSelfTo<LevelSliderPresenter>().AsSingle();
        }

        private void BindVictoryUI()
        {
            Container.BindInstance(victoryInfoView);
            Container.BindInterfacesAndSelfTo<VictoryInfoPresenter>().AsSingle();
        }

        private void BindMainUI() =>
            Container.BindInstance(_mainUI);

        private void BindWeaponSelectorViewsProvider()
        {
            Container
                .BindInterfacesTo<WeaponSelectorViewsProvider>()
                .FromInstance(_weaponSelectorViewsProvider);
        }

        private void BindBuyWeaponUI()
        {
            Container.BindInstance(_buyButtonView);
            Container.BindInterfacesAndSelfTo<BuyWeaponPresenter>()
                .AsSingle();
        }

        private void BindShopWeaponUI()
        {
            Container
                .BindInterfacesAndSelfTo<ShopWeaponPresenter>()
                .AsSingle();

            Container.BindInstance(shopWeaponInfoView);
        }

        private void BindScrollUI()
        {
            Container
                .BindInterfacesTo<ScrollRectProvider>()
                .FromInstance(_scrollRectProvider);

            Container
                .BindInterfacesAndSelfTo<ScrollRectPresenter>()
                .AsSingle();

            Container.Bind<ScrollRectChanger>()
                .AsSingle();

            Container.Bind<ScrollUnderlineChanger>()
                .AsSingle();

            Container
                .BindInterfacesTo<ScrollImagesProvider>()
                .FromInstance(_scrollImageProvider);

            Container
                .BindInterfacesAndSelfTo<BlockScrollButtonsOnChange>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ScrollUnderlinePresenter>()
                .AsSingle();

            Container.BindInterfacesTo<ScrollNameUnderlinesProvider>()
                .FromInstance(_scrollNameUnderlinesProvider);
        }

        private void BindWallet()
        {
            Container
                .BindInterfacesAndSelfTo<WalletPresenter>()
                .AsSingle();

            Container.Bind<Wallet>()
                .AsSingle();

            Container.BindInstance(_walletUI);
        }

        private void BindPlayUI()
        {
            Container
                .BindInterfacesAndSelfTo<PlayWindowPresenter>()
                .AsSingle();
        }

        private void BindDeathUI()
        {
            Container.BindInstance(_deathView);
            Container.BindInterfacesAndSelfTo<DeathPresenter>()
                .AsSingle();
        }

        private void BindSettingUi()
        {
            Container.BindInterfacesAndSelfTo<SettingPresenter>().AsSingle();
            Container.BindInstance(_settingView);
        }

        private void BindAudioUI()
        {
            Container.BindInstance(_audioView);
            Container.BindInterfacesAndSelfTo<AudioPresenter>().AsSingle();
            Container.Bind<AudioChanger>().AsSingle();
        }

        private void BindPlayerSelector()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerSelectorPresenter>()
                .AsSingle();

            Container
                .Bind<PlayerSelector>()
                .AsSingle();
        }

        private void BindShopUI()
        {
            Container.BindInstance(_shopView);
            Container
                .BindInterfacesAndSelfTo<ShopPresenter>()
                .AsSingle();
            Container.BindInstance(_shopMoneyText);
        }

        private void BindWeaponIconsProvider()
        {
            Container
                .BindInterfacesAndSelfTo<WeaponIconsProvider>()
                .FromInstance(_weaponIconsProvider);
        }

        private void BindUIFactory()
        {
            Container
                .Bind<UIFactory>()
                .AsSingle();
        }

        private void BindWindowProvider()
        {
            Container
                .BindInstance(_windowProvider)
                .AsSingle();
        }

        private void BindWindowService()
        {
            Container
                .Bind<WindowService>()
                .AsSingle();
        }
    }
}