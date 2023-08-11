using System.Collections.Generic;
using CodeBase.Gameplay.BlockInput;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.EffectPlaying;
using CodeBase.Gameplay.EnemyBodyParts;
using CodeBase.Gameplay.Loots;
using CodeBase.Gameplay.Spawners;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Data;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
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
        [SerializeField] private EnemyQuantityZonesProvider _enemyQuantityZonesProvider;
        [SerializeField] private TargetMovementStorage _targetMovementStorage;

        public override void InstallBindings()
        {
            BindLocationProvider();
            BindGameInit();
            BindCameraFactory();
            BindPlayerFactory();
            BindCameraProvider();
            BindPlayerProvider();
            BindWeaponFactory();
            BindWeaponsProvider();
            BindGameObjectPoolProvider();
            BindBulletStorage();
            BindWeaponSelection();
            BindPlayerStorage();
            BindWeaponStorage();
            BindEnemyFactory();
            BindSoundStorage();
            BindSoundFactory();
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
            BindWeaponShootEffectStorage();
            BindStaticDataServices();
            BindEnemyQuantityZonesProvider();
            BindTargetMovementStorage();
            BindLootStorage();
            BindActivateEnemiesMovementOnFire();
            BindBlockInputOnLoading();
            BindLootFactory();
        }

        private void BindAsSingle<T>() =>
            Container
                .Bind<T>()
                .AsSingle();

        private void BindLootFactory() =>
            BindAsSingle<LootFactory>();

        private void BindBlockInputOnLoading() =>
            Container
                .BindInterfacesAndSelfTo<EnableInputOnPlayWindow>()
                .AsSingle();

        private void BindActivateEnemiesMovementOnFire() =>
            Container
                .BindInterfacesAndSelfTo<ActivateEnemiesMovementOnFire>()
                .AsSingle();

        private void BindLootStorage() =>
            BindAsSingle<LootStorage>();

        private void BindTargetMovementStorage() =>
            Container.BindInstance(_targetMovementStorage);

        private void BindStaticDataServices()
        {
            BindAsSingle<BulletStaticDataService>();
            BindAsSingle<EnemyStaticDataService>();
            BindAsSingle<PlayerStaticDataService>();
            BindAsSingle<WeaponStaticDataService>();
            BindAsSingle<BulletEffectStorage>();
        }

        private void BindWeaponShootEffectStorage() =>
            BindAsSingle<WeaponShootEffectStorage>();

        private void BindEnemiesDeathEffectOnQuickDestruction() =>
            BindAsSingle<EnemiesDeathEffectOnDestruction>();

        private void BindEnemyEffectDataStorage() =>
            BindAsSingle<EnemyEffectDataStorage>();

        private void BindDestroyableObjectStorages()
        {
            Container.BindInstance(_destroyableObjectStorage);
            Container.BindInstance(_destroyableObjectPartStorage);
        }

        private void BindEnemyQuantityZonesProvider() =>
            Container.BindInstance(_enemyQuantityZonesProvider);

        private void BindBulletMovementStorage() =>
            BindAsSingle<BulletMovementStorage>();

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
            BindAsSingle<EnemyConfigurator>();
            Container.BindInterfacesAndSelfTo<EnemyConfiguratorMediator>().AsSingle();
        }

        private void BindEnemyBodyPartStorage() =>
            Container
                .BindInstance(_enemyBodyPartStorage);

        private void BindEnemyBodyPartActivation()
        {
            BindAsSingle<EnemyBodyPartPositionSetter>();
            BindAsSingle<EnemyBodyPartActivator>();
            Container.BindInterfacesAndSelfTo<EnemyBodyPartMediator>().AsSingle();
        }

        private void BindEnemyStorage()
        {
            Container.Bind<IEnemyStorage>()
                .To<EnemyStorage>()
                .AsSingle();
        }

        private void BindSoundFactory() =>
            Container.Bind<IEffectFactory>()
                .To<EffectFactory>()
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
            BindAsSingle<WeaponSelector>();
            Container.BindInterfacesAndSelfTo<WeaponSelectorPresenter>().AsSingle();
        }

        private void BindBulletStorage() =>
            BindAsSingle<BulletStorage>();

        private void BindWeaponsProvider() =>
            Container.Bind<IProvider<Weapon>>()
                .To<WeaponProvider>()
                .AsSingle();

        private void BindGameObjectPoolProvider() =>
            BindAsSingle<GameObjectPoolProvider>();

        private void BindWeaponFactory() =>
            BindAsSingle<WeaponFactory>();

        private void BindPlayerProvider() =>
            BindAsSingle<PlayerProvider>();

        private void BindCameraProvider() =>
            BindAsSingle<CameraProvider>();

        private void BindPlayerFactory() =>
            BindAsSingle<PlayerFactory>();

        private void BindCameraFactory() =>
            BindAsSingle<PlayerCameraFactory>();

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