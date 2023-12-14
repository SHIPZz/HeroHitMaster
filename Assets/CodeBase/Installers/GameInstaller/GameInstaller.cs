using System.Collections.Generic;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Character.Players.Shoot;
using CodeBase.Gameplay.EffectsData;
using CodeBase.Gameplay.EnemyBodyParts;
using CodeBase.Gameplay.Loots;
using CodeBase.Gameplay.MusicHandlerSystem;
using CodeBase.Gameplay.Spawners;
using CodeBase.Gameplay.WaterSplash;
using CodeBase.Infrastructure;
using CodeBase.Services;
using CodeBase.Services.AccuracyCounters;
using CodeBase.Services.Cheats;
using CodeBase.Services.CheckOut;
using CodeBase.Services.Data;
using CodeBase.Services.Factories;
using CodeBase.Services.GameContinues;
using CodeBase.Services.GameRestarters;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.SaveTriggers;
using CodeBase.Services.SaveSystems.WeaponSaves;
using CodeBase.Services.Slowmotion;
using CodeBase.Services.Storages.Bullet;
using CodeBase.Services.Storages.Character;
using CodeBase.Services.Storages.Effect;
using CodeBase.Services.Storages.Sound;
using CodeBase.Services.Storages.Weapon;
using CodeBase.UI;
using CodeBase.UI.Weapons;
using CodeBase.UI.Weapons.ShopWeapons;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Play;
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
        [SerializeField] private SaveTriggerOnLevelEnd _saveTriggerOnLevel;
        [SerializeField] private ExplosionBarrelsProvider _explosionBarrelsProvider;
        [SerializeField] private Level _level;

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
            BindLootFactory();
            BindSaveTriggers();
            BindCheckOutService();
            BindSlowMotion();
            BindEffectStorage();
            BindEffectProvider();
            BindObjectPartFactory();
            BindEnemyPartFactory();
            BindCountEnemiesOnDeath();
            BindWaterSplashPoolInitializer();
            BindEffectsPoolProvider();
            BindCamerShake();
            BindExplosionBarrelsProvider();
            BindRotateCameraOnEnemyKill();
            BincCameraZoomer();
            BindRotateCameraPresenter();
            BindLevel();
            BindPlaySoundOnFocusChanged();
            BindAdWatchCounter();
            BindWeaponSaver();
            BindGameRestarter();
            BindGameContinue();
            BindKillActiveEnemiesOnPlayerRecover();
            BindEnemyProvider();
            BindAccuracyCounter();
            BindGameplayRunner();
            BindGamePartsInitializers();
            BindFocusService();
            BindMusicHandler();
            Container.BindInterfacesAndSelfTo<CheatService>().AsSingle();
        }

        private void BindMusicHandler()
        {
            Container
                .BindInterfacesAndSelfTo<MusicHandler>()
                .AsSingle();
        }

        private void BindFocusService() =>
            Container
                .BindInterfacesTo<FocusService>()
                .AsSingle();

        private void BindGamePartsInitializers()
        {
            Container.BindInterfacesAndSelfTo<EnemiesMovementInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerShootInputMediator>().AsSingle();
        }

        private void BindGameplayRunner()
        {
            Container.BindInterfacesAndSelfTo<GameplayRunner>().AsSingle();
        }

        private void BindAccuracyCounter()
        {
            Container
                .BindInterfacesAndSelfTo<AccuracyCounter>()
                .AsSingle();
        }

        private void BindEnemyProvider() =>
            Container
                .Bind<IEnemyProvider>()
                .To<EnemyProvider>()
                .AsSingle();

        private void BindKillActiveEnemiesOnPlayerRecover() =>
            BindAsSingle<KillActiveEnemiesOnPlayerRecover>();

        private void BindGameContinue()
        {
            Container.BindInterfacesAndSelfTo<AdRewardPresenter>().AsSingle();
            BindAsSingle<AdReward>();
        }

        private void BindGameRestarter() =>
            Container.BindInterfacesAndSelfTo<GameRestarterMediator>().AsSingle();

        private void BindWeaponSaver()
        {
            Container.BindInterfacesAndSelfTo<WeaponSaverMediator>().AsSingle();
            BindAsSingle<WeaponSaver>();
        }

        private void BindAdWatchCounter()
        {
            BindAsSingle<WeaponAdWatchCounter>();
            Container.BindInterfacesAndSelfTo<AdWatchCounterPresenter>().AsSingle();
        }

        private void BindPlaySoundOnFocusChanged() =>
            Container
                .BindInterfacesAndSelfTo<PlaySoundOnFocusChanged>()
                .AsSingle();

        private void BindLevel() =>
            Container.BindInstance(_level);

        private void BindRotateCameraPresenter() =>
            Container
                .BindInterfacesAndSelfTo<RotateCameraPresenter>()
                .AsSingle();

        private void BincCameraZoomer()
        {
            BindAsSingle<CameraZoomer>();
        }

        private void BindRotateCameraOnEnemyKill() =>
            Container.BindInterfacesAndSelfTo<RotateCameraOnLastEnemyKilled>().AsSingle();

        private void BindExplosionBarrelsProvider() =>
            Container.BindInterfacesAndSelfTo<ExplosionBarrelsProvider>()
                .FromInstance(_explosionBarrelsProvider).AsSingle();

        private void BindCamerShake() =>
            Container.BindInterfacesAndSelfTo<CameraShakeMediator>()
                .AsSingle();

        private void BindEffectsPoolProvider() =>
            Container
                .BindInterfacesTo<EffectsPoolProvider>()
                .AsSingle();

        private void BindWaterSplashPoolInitializer() =>
            BindAsSingle<WaterSplashPoolInitializer>();

        private void BindCountEnemiesOnDeath() =>
            Container.BindInterfacesAndSelfTo<CountEnemiesOnDeath>()
                .AsSingle();

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
            Container.BindInstance(_saveTriggerOnLevel);
        }

        private void BindAsSingle<T>() =>
            Container
                .Bind<T>()
                .AsSingle();

        private void BindLootFactory() =>
            BindAsSingle<LootFactory>();

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
            Container.BindInterfacesAndSelfTo<WeaponSelectorMediator>().AsSingle();
        }

        private void BindBulletStorage() =>
            BindAsSingle<BulletStorage>();

        private void BindWeaponsProvider() =>
            Container.BindInterfacesTo<WeaponProvider>()
                .AsSingle();

        private void BindGameObjectPoolProvider() =>
            Container.BindInterfacesAndSelfTo<BulletsPoolProvider>()
                .AsSingle();

        private void BindWeaponFactory() =>
            BindAsSingle<WeaponFactory>();

        private void BindPlayerProvider() =>
            Container
                .BindInterfacesTo<PlayerProvider>()
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