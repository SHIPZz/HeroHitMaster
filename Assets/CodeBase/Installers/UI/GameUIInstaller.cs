using System.Collections.Generic;
using CodeBase.Gameplay.Character.PlayerSelection;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
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
using CodeBase.UI.Windows.Setting;
using CodeBase.UI.Windows.Shop;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.UI
{
    public class GameUIInstaller : MonoInstaller
    {
        [SerializeField] private WindowProvider _windowProvider;
        [SerializeField] private WeaponIconsProvider _weaponIconsProvider;
        [SerializeField] private List<WeaponSelectorView> _weaponSelectorViews;
        [SerializeField] private AudioView _audioView;
        [SerializeField] private ShopView _shopView;
        [SerializeField] private SettingView _settingView;
        [SerializeField] private DeathView _deathView;
        [SerializeField] private WalletUI _walletUI;
        [SerializeField] private ScrollRectProvider _scrollRectProvider;
        [SerializeField] private ScrollImagesProvider _scrollImageProvider;
        [SerializeField] private ScrollNameUnderlinesProvider _scrollNameUnderlinesProvider;
        [SerializeField] private ShopWeaponInfo _shopWeaponInfo;
        [SerializeField] private ShopMoneyText _shopMoneyText;
        [SerializeField] private BuyButtonView _buyButtonView;

        public override void InstallBindings()
        {
            BindWindowProvider();
            BindWindowService();
            BindUIFactory();
            BindWeaponIconsProvider();
            BindShopUI();
            BindPlayerSelector();
            BindWeaponSelectorViews();
            BindAudioUI();
            BindSettingUi();
            BindDeathUI();
            BindPlayUI();
            BindWallet();
            BindScrollUI();
            BindShopWeaponUI();
            BindBuyWeaponUI();
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

            Container.BindInstance(_shopWeaponInfo);
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

        private void BindWeaponSelectorViews()
        {
            Container
                .BindInstance(_weaponSelectorViews);
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