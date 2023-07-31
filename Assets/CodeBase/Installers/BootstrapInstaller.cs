using CodeBase.Services.Inputs.InputService;
using CodeBase.Services.Providers.AssetProviders;
using CodeBase.UI;
using UnityEngine.InputSystem;
using Zenject;

namespace CodeBase.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAssetProvider();
            BindInputService();
            BindAdService();
        }

        private void BindAdService()
        {
            Container.BindInterfacesAndSelfTo<YandexAdService>()
                .AsSingle();
        }

        private void BindInputService()
        {
            Container
                .Bind<InputActions>()
                .AsSingle();
            Container
                .BindInterfacesTo<InputService>()
                .AsSingle();
        }

        private void BindAssetProvider()
        {
           var assetProvider = new AssetProvider();
            Container
                .Bind<AssetProvider>()
                .FromInstance(assetProvider)
                .AsSingle();
        }
    }
}