using CodeBase.Services;
using CodeBase.Services.Ad;
using CodeBase.Services.Inputs.InputService;
using CodeBase.Services.Providers.AssetProviders;
using UnityEngine;
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
            BindCoroutineStarter();
        }

        private void BindCoroutineStarter() => 
            Container.BindInstance(GetComponent<ICoroutineStarter>());

        private void BindAdService() =>
            Container.BindInterfacesAndSelfTo<YandexAdService>()
                .AsSingle();

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
           assetProvider.Initialize();
            Container
                .Bind<AssetProvider>()
                .FromInstance(assetProvider)
                .AsSingle();
        }
    }
}