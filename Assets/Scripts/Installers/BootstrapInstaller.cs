using Services.Providers.AssetProviders;
using UnityEngine.InputSystem;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAssetProvider();
            BindInputService();
        }

        private void BindInputService()
        {
            Container.Bind<InputActions>().AsSingle();
            Container.BindInterfacesTo<InputService>().AsSingle();
        }

        private void BindAssetProvider()
        {
           var assetProvider = new AssetProvider();
            Container.Bind<AssetProvider>().FromInstance(assetProvider).AsSingle();
        }
    }
}