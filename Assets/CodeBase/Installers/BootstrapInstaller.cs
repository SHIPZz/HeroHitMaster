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

        private void BindInputService() =>
            Container
                .BindInterfacesAndSelfTo<InputService>()
                .AsSingle();

        private void BindAssetProvider()
        {
            Container
                .BindInterfacesAndSelfTo<AssetProvider>()
                .AsSingle();
        }
    }
}