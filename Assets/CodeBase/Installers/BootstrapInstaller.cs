using CodeBase.Services;
using CodeBase.Services.Ad;
using CodeBase.Services.Data;
using CodeBase.Services.Inputs.InputService;
using CodeBase.Services.Providers.AssetProviders;
using CodeBase.Services.Storages;
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
            BindStaticDataServices();
        }

        private void BindStaticDataServices()
        {
            Container.Bind<BulletStaticDataService>().AsSingle();
            Container.Bind<EnemyStaticDataService>().AsSingle();
            Container.Bind<PlayerStaticDataService>().AsSingle();
            Container.Bind<WeaponStaticDataService>().AsSingle();
        }

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
            Container
                .Bind<AssetProvider>()
                .FromInstance(assetProvider)
                .AsSingle();
        }
    }
}