using Gameplay.Web;
using UnityEngine;
using Zenject;

namespace Installers.GameObjectInstallers
{
    public class WebInstaller : MonoInstaller
    {
        [SerializeField] private Web _web;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_web);
            Container.Bind<WebMovement>().AsSingle();
            Container.Bind<ShootMediator>().AsSingle();
        }
    }
}