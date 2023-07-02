using Gameplay.PlayerSelection;
using Gameplay.WeaponSelection;
using Services.Providers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameUIInstaller : MonoInstaller
    {
        [SerializeField] private WeaponSelectorView _weaponSelectorView;
        [SerializeField] private PlayerSelectorView _playerSelectorView;
        [SerializeField] private WindowProvider _windowProvider;
        
        public override void InstallBindings()
        {
            BindWeaponSelectorView();
            BindPlayerSelectorView();
            BindWindowProvider();
            BindWindowService();
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

        private void BindWeaponSelectorView()
        {
            Container
                .BindInstance(_weaponSelectorView);
        }
        
        private void BindPlayerSelectorView()
        {
            Container
                .BindInstance(_playerSelectorView);
        }
    }
}