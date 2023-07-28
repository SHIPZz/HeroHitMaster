using System.Collections.Generic;
using Windows;
using Windows.Shop;
using Services.Factories;
using Services.Providers;
using UnityEngine;
using Weapons;
using Zenject;

namespace Installers.UI
{
    public class GameUIInstaller : MonoInstaller
    {
        [SerializeField] private WindowProvider _windowProvider;
        [SerializeField] private WeaponIconsProvider _weaponIconsProvider;
        [SerializeField] private ShopView _shopView;
        [SerializeField] private List<WeaponSelectorView> _weaponSelectorViews;

        public override void InstallBindings()
        {
            BindWindowProvider();
            BindWindowService();
            BindUIFactory();
            BindWeaponIconsProvider();
            BindShopUI();
            BindPlayerSelector();
            BindWeaponSelectorViews();
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
            Container
                .BindInterfacesAndSelfTo<ShopPresenter>()
                .AsSingle();

            Container
                .BindInstance(_shopView);
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