using CodeBase.Gameplay.BlockInput;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.EnemyBodyParts;
using CodeBase.Gameplay.Loots;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.CheckOut;
using CodeBase.Services.Data;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems.SaveTriggers;
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
            BindEnemyBodyPartStorage();
            BindEnemySpawnerProvider();
            BindMaterialProvider();
            BindSetterWeapon();
            BindBulletMovementStorage();
            BindDestroyableObjectStorages();
            BindEnemyEffectDataStorage();
            BindWeaponShootEffectStorage();
            BindStaticDataServices();
            BindEnemyQuantityZonesProvider();
            BindTargetMovementStorage();
            BindLootStorage();
            BindBlockInputOnLoading();
            BindLootFactory();
            BindBlockInputOnWindowsOpening();
            BindSaveTriggers();
            BindCheckOutService();
        }

        private void BindCheckOutService() =>
            Container
                .Bind<CheckOutService>()
                .AsSingle();

        private void BindSaveTriggers()
        {
            Container.BindInterfacesAndSelfTo<SoundSaveOnTrigger>().AsSingle();
            Container.BindInterfacesAndSelfTo<PurchasedWeaponsSaveTrigger>().AsSingle();
            Container.BindInterfacesAndSelfTo<PurchasedWeaponsTriggerPresenter>().AsSingle();
        }

        private void BindBlockInputOnWindowsOpening()
        {
            Container.BindInterfacesAndSelfTo<BlockShootInputOnUI>()
                .AsSingle();
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
            BindAsSingle<WalletStaticDataService>();
        }

        private void BindWeaponShootEffectStorage() =>
            BindAsSingle<WeaponShootEffectStorage>();

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
            Container.BindInterfacesAndSelfTo<MaterialProvider>()
                .FromInstance(_materialProvider).AsSingle();

        private void BindEnemySpawnerProvider()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawnersProvider>()
                .FromInstance(_enemySpawnersProvider).AsSingle();
        }

        private void BindEnemyBodyPartStorage() =>
            Container
                .BindInstance(_enemyBodyPartStorage);

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
            Container.BindInterfacesAndSelfTo<BulletsPoolProvider>()
                .AsSingle();

        private void BindWeaponFactory() =>
            BindAsSingle<WeaponFactory>();

        private void BindPlayerProvider() =>
            Container
                .Bind<IProvider<Player>>()
                .To<PlayerProvider>()
                .AsSingle();

        private void BindCameraProvider() =>
            Container
                .BindInterfacesAndSelfTo<CameraProvider>()
                .AsSingle();

        private void BindPlayerFactory() =>
            BindAsSingle<PlayerFactory>();

        private void BindCameraFactory() =>
            BindAsSingle<PlayerCameraFactory>();

        private void BindGameInit() =>
            Container
                .BindInterfacesAndSelfTo<GameInit.GameInit>()
                .AsSingle();

        private void BindLocationProvider() =>
            Container
                .BindInterfacesTo<LocationProvider>()
                .FromInstance(_locationProvider)
                .AsSingle();
    }
}