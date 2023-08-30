using System.Collections.Generic;
using CodeBase.Gameplay.BlockInput;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.EnemyBodyParts;
using CodeBase.Gameplay.Loots;
using CodeBase.Gameplay.Spawners;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.CheckOut;
using CodeBase.Services.Data;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems.SaveTriggers;
using CodeBase.Services.Slowmotion;
using CodeBase.Services.Storages.Bullet;
using CodeBase.Services.Storages.Character;
using CodeBase.Services.Storages.Effect;
using CodeBase.Services.Storages.Sound;
using CodeBase.Services.Storages.Weapon;
using CodeBase.UI.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.GameInstaller
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private EnemySpawnersProvider _enemySpawnersProvider;
        [SerializeField] private LocationProvider _locationProvider;
        [SerializeField] private MaterialProvider _materialProvider;
        [SerializeField] private EnemyQuantityZonesProvider _enemyQuantityZonesProvider;
        [SerializeField] private TargetMovementStorage _targetMovementStorage;
        [SerializeField] private EffectsProvider _effectsProvider;

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
            BindEnemySpawnerProvider();
            BindMaterialProvider();
            BindSetterWeapon();
            BindEnemyEffectDataStorage();
            BindWeaponShootEffectStorage();
            BindStaticDataServices();
            BindEnemyQuantityZonesProvider();
            BindTargetMovementStorage();
            BindLootStorage();
            BindBlockInputOnLoading();
            BindLootFactory();
            BindBlockInputOnUi();
            BindSaveTriggers();
            BindCheckOutService();
            BindSlowMotion();
            BindEffectStorage();
            BindEffectProvider();
            BindObjectPartFactory();
            BindEnemyPartFactory();
        }

        private void BindEnemyPartFactory() => 
            BindAsSingle<EnemyPartFactory>();

        private void BindObjectPartFactory() =>
            BindAsSingle<ObjectPartFactory>();

        private void BindEffectProvider() =>
            Container
                .BindInstance(_effectsProvider);

        private void BindEffectStorage() =>
            Container.Bind<IEffectStorage>().To<EffectStorage>().AsSingle();

        private void BindSlowMotion() =>
            Container.BindInterfacesAndSelfTo<SlowMotionOnEnemyDeath>()
                .AsSingle();

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

        private void BindBlockInputOnUi()
        {
            Container.Bind<BlockShootInput>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<BlockShootInputPresenter>()
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
            BindAsSingle<SoundStaticDataService>();
            BindAsSingle<EffectStaticDataService>();
        }

        private void BindWeaponShootEffectStorage() =>
            BindAsSingle<WeaponShootEffectStorage>();

        private void BindEnemyEffectDataStorage() =>
            BindAsSingle<EnemyEffectDataStorage>();

        private void BindEnemyQuantityZonesProvider() =>
            Container.BindInstance(_enemyQuantityZonesProvider);

        private void BindSetterWeapon() =>
            Container.BindInterfacesAndSelfTo<SetterWeaponToPlayerHand>().AsSingle();

        private void BindMaterialProvider() =>
            Container.BindInterfacesAndSelfTo<MaterialProvider>()
                .FromInstance(_materialProvider).AsSingle();

        private void BindEnemySpawnerProvider()
        {
            Container.Bind<IProvider<List<EnemySpawner>>>().To<EnemySpawnersProvider>()
                .FromInstance(_enemySpawnersProvider).AsSingle();
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