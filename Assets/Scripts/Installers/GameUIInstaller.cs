using Gameplay.PlayerSelection;
using Services.Factories;
using Services.Providers;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameUIInstaller : MonoInstaller
    {
        [SerializeField] private PlayerSelectorView _playerSelectorView;
        [SerializeField] private WindowProvider _windowProvider;

        public override void InstallBindings()
        {
            BindPlayerSelectorView();
            BindWindowProvider();
            BindWindowService();
            BindUIFactory();
            BindWeaponIconsProvider();
        }

        private void BindWeaponIconsProvider()
        {
            Container
                .Bind<WeaponIconsProvider>()
                .AsSingle();
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

        private void BindPlayerSelectorView()
        {
            Container
                .BindInstance(_playerSelectorView);
        }
    }
}