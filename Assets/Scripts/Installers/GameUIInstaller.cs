using Services;
using Services.WeaponSelection;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameUIInstaller : MonoInstaller
    {
        [SerializeField] private WeaponSelectorView weaponSelectorView;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(weaponSelectorView);
        }
    }
}