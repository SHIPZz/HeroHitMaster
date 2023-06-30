using Gameplay.PlayerSelection;
using Gameplay.WeaponSelection;
using Services.Factories;
using Services.Providers;
using UnityEngine;
using Zenject;

namespace Installers.GameInstaller
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _cameraTransformPoint;

        public override void InstallBindings()
        {
            BindLocationProvider();
            BindGameInit();
            BindCameraFactory();
            BindPlayerFactory();
            BindGameFactory();
            BindCameraProvider();
            BindPlayerProvider();
            BindWeaponFactory();
            BindWeaponsProvider();
            BindGameObjectPoolProvider();
            BindBulletFactory();
            BindWeaponSelection();
            BindPlayerSelection();
        }

        private void BindPlayerSelection()
        {
            Container
                .Bind<PlayerSelector>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PlayerSelectorPresenter>()
                .AsSingle();
        }

        private void BindWeaponSelection()
        {
            Container
                .Bind<WeaponSelector>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<WeaponSelectorPresenter>()
                .AsSingle();
        }

        private void BindBulletFactory() =>
            Container
                .Bind<BulletFactory>()
                .AsSingle();

        private void BindWeaponsProvider() =>
            Container
                .Bind<WeaponsProvider>().AsSingle();

        private void BindGameObjectPoolProvider() =>
            Container
                .Bind<GameObjectPoolProvider>().AsSingle();

        private void BindWeaponFactory() =>
            Container.Bind<WeaponFactory>().AsSingle();

        private void BindPlayerProvider() =>
            Container
                .Bind<PlayerProvider>()
                .AsSingle();

        private void BindCameraProvider() =>
            Container
                .Bind<CameraProvider>().AsSingle();

        private void BindGameFactory() =>
            Container
                .Bind<GameFactory>()
                .AsSingle();

        private void BindPlayerFactory() =>
            Container
                .Bind<PlayerFactory>()
                .AsSingle();

        private void BindCameraFactory() =>
            Container
                .Bind<PlayerCameraFactory>()
                .AsSingle();

        private void BindGameInit() =>
            Container
                .BindInterfacesAndSelfTo<GameInit.GameInit>()
                .AsSingle();

        private void BindLocationProvider()
        {
            LocationProvider locationProvider = new(_spawnPoint, _cameraTransformPoint);
            Container
                .BindInstance(locationProvider)
                .AsSingle();
        }
    }
}