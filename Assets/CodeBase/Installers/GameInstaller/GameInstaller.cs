using System.Collections.Generic;
using CodeBase.Gameplay;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.EffectPlaying;
using CodeBase.Gameplay.EnemyBodyParts;
using CodeBase.Gameplay.ObjectBodyPart;
using CodeBase.Gameplay.Sound;
using CodeBase.Gameplay.Spawners;
using CodeBase.Services.Data;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Bullet;
using CodeBase.Services.Storages.Character;
using CodeBase.Services.Storages.ObjectParts;
using CodeBase.Services.Storages.Sound;
using CodeBase.Services.Storages.Weapon;
using CodeBase.UI.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.GameInstaller
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private EnemyBodyPartStorage _enemyBodyPartStorage;
        [SerializeField] private EnemySpawnersProvider _enemySpawnersProvider;
        [SerializeField] private LocationProvider _locationProvider;
        [SerializeField] private MaterialProvider _materialProvider;
        [SerializeField] private DestroyableObjectStorage _destroyableObjectStorage;
        [SerializeField] private DestroyableObjectPartStorage _destroyableObjectPartStorage;

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
            BindPlayerStorage();
            BindWeaponStorage();
            BindEnemyFactory();
            BindSoundStorage();
            BindSoundFactory();
            BindSound();
            BindSoundProvider();
            BindEnemyStorage();
            BindEnemyBodyPartActivation();
            BindEnemyBodyPartStorage();
            BindEnemyConfiguration();
            BindEnemySpawnerProvider();
            BindMaterialProvider();
            BindSetterWeapon();
            BindBulletMovementStorage();
            BindDestroyableObjectStorages();
            BindEnemyEffectDataStorage();
            BindEnemiesDeathEffectOnQuickDestruction();
        }

        private void BindEnemiesDeathEffectOnQuickDestruction() => 
            Container.BindInterfacesAndSelfTo<EnemiesDeathEffectOnDestruction>().AsSingle();

        private void BindEnemyEffectDataStorage() => 
            Container.Bind<EnemyEffectDataStorage>().AsSingle();

        private void BindDestroyableObjectStorages()
        {
            Container.BindInstance(_destroyableObjectStorage);
            Container.BindInstance(_destroyableObjectPartStorage);
        }

        private void BindBulletMovementStorage() =>
            Container.Bind<BulletMovementStorage>()
                .AsSingle();

        private void BindSetterWeapon() => 
            Container.BindInterfacesAndSelfTo<SetterWeaponToPlayerHand>().AsSingle();

        private void BindMaterialProvider() => 
            Container.BindInstance(_materialProvider);

        private void BindEnemySpawnerProvider()
        {
            Container
                .BindInstance(_enemySpawnersProvider);
            Container.BindInterfacesAndSelfTo<List<EnemySpawner>>().FromInstance(_enemySpawnersProvider.EnemySpawners);
        }

        private void BindEnemyConfiguration()
        {
            Container.Bind<EnemyConfigurator>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyConfiguratorMediator>().AsSingle();
        }

        private void BindEnemyBodyPartStorage() =>
            Container
                .BindInstance(_enemyBodyPartStorage);

        private void BindEnemyBodyPartActivation()
        {
            Container.Bind<EnemyBodyPartActivator>().AsSingle();
            Container.Bind<EnemyBodyPartPositionSetter>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyBodyPartMediator>().AsSingle();
        }

        private void BindEnemyStorage()
        {
            Container.Bind<IEnemyStorage>()
                .To<EnemyStorage>()
                .AsSingle();
        }

        private void BindSoundProvider() => 
            Container.Bind<SoundProvider>().AsSingle();

        private void BindSound()
        {
            Container.Bind<SoundWeaponChanger>().AsSingle();
            Container.BindInterfacesAndSelfTo<SoundWeaponPresenter>().AsSingle();
        }

        private void BindSoundFactory() =>
            Container.Bind<ISoundFactory>()
                .To<SoundFactory>()
                .AsSingle();

        private void BindSoundStorage() =>
            Container
                .BindInterfacesAndSelfTo<SoundStorage>()
                .AsSingle();

        private void BindEnemyFactory() =>
            Container.Bind<EnemyFactory>()
                .AsSingle();

        private void BindWeaponStorage() =>
            Container
                .Bind<IWeaponStorage>()
                .To<WeaponStorage>()
                .AsSingle();

        private void BindPlayerStorage() =>
            Container
                .Bind<IPlayerStorage>()
                .To<PlayerStorage>()
                .AsSingle();

        private void BindWeaponSelection()
        {
            Container.Bind<WeaponSelector>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponSelectorPresenter>().AsSingle();
        }

        private void BindBulletFactory() =>
            Container
                .Bind<BulletStorage>()
                .AsSingle();

        private void BindWeaponsProvider() =>
            Container
                .Bind<WeaponProvider>().AsSingle();

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
            Container
                .BindInstance(_locationProvider)
                .AsSingle();
        }
    }
}