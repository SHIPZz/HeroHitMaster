using Gameplay.PlayerSelection;
using Gameplay.WeaponSelection;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameUIInstaller : MonoInstaller
    {
        [SerializeField] private WeaponSelectorView _weaponSelectorView;
        [SerializeField] private PlayerSelectorView _playerSelectorView;
        
        public override void InstallBindings()
        {
            BindWeaponSelectorView();

            BindPlayerSelectorView();
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