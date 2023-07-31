using System.Collections.Generic;
using CodeBase.Gameplay.Character.PlayerSelection;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.UI.Weapons;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Audio;
using CodeBase.UI.Windows.Death;
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