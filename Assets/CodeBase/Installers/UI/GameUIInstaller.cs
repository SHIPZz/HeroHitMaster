using System.Collections.Generic;
using Windows;
using Windows.Audio;
using Windows.Setting;
using Windows.Shop;
using CodeBase.Gameplay.Character.PlayerSelection;
using Services.Factories;
using Services.Providers;
using UnityEngine;
using Weapons;
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
        }

        private void BindWeaponIconsProvider()
        {
            Container
                .BindInstance(_weaponIconsProvider);
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