using Gameplay.PlayerSelection;
using Services.Factories;
using Services.GameObjectsPoolAccess;
using Services.Providers;
using UI;
using UnityEngine;
using Zenject;

namespace Installers.GameInstaller
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _cameraTransformPoint;
        [SerializeField] private Transform _playerParentTransform;
        [SerializeField] private Transform _weaponIconParentTransform;

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
            BindPlayerSelection();
            BindPlayerFactoryByWeaponType();
            BindWeaponSelection();
            BindPlayerStorage();
            BindPlayerSwitcher();
        }

        private void BindPlayerSwitcher()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerSwitcherHandler>()
                .AsSingle();
        }

        private void BindPlayerStorage()
        {
            Container
                .Bind<PlayerStorage>()
                .AsSingle();
        }

        private void BindWeaponSelection()
        {
            Container.Bind<WeaponCreator>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponSelectorPresenter>().AsSingle();
        }

        private void BindPlayerFactoryByWeaponType()
        {
            Container
                .Bind<PlayerStorageByWeaponType>()
                .AsSingle();
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
            LocationProvider locationProvider = new(_playerParentTransform, 
                _spawnPoint,_cameraTransformPoint,_weaponIconParentTransform);
            
            Container
                .BindInstance(locationProvider)
                .AsSingle();
        }
    }
}