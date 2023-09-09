using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Spawners;
using CodeBase.Gameplay.WaterSplash;
using CodeBase.Infrastructure;
using CodeBase.Services;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.Slowmotion;
using CodeBase.Services.Storages.Character;
using CodeBase.UI.LevelSlider;
using CodeBase.UI.Wallet;
using CodeBase.UI.Weapons;
using CodeBase.UI.Weapons.ShopWeapons;
using UnityEngine;
using Zenject;
using Player = CodeBase.Gameplay.Character.Players.Player;

namespace CodeBase.GameInit
{
    public class GameInit : IInitializable
    {
        private readonly PlayerCameraFactory _playerCameraFactory;
        private readonly IProvider<LocationTypeId, Transform> _locationProvider;
        private readonly IProvider<Camera> _cameraProvider;
        private readonly IProvider<Player> _playerProvider;
        private readonly IPlayerStorage _playerStorage;
        private readonly WeaponSelector _weaponSelector;
        private readonly WalletPresenter _walletPresenter;
        private readonly ISaveSystem _saveSystem;
        private readonly ShopWeaponPresenter _shopWeaponPresenter;
        private readonly List<EnemySpawner> _enemySpawners;
        private readonly EnemyConfigurator _enemyConfigurator = new();
        private readonly CountEnemiesOnDeath _countEnemiesOnDeath;
        private readonly SlowMotionOnEnemyDeath _slowMotionOnEnemyDeath;
        private readonly LevelSliderPresenter _levelSliderPresenter;
        private readonly WaterSplashPoolInitializer _waterSplashPoolInitializer;
        private CameraShakeMediator _cameraShakeMediator;
        private RotateCameraPresenter _rotateCameraPresenter;

        public GameInit(PlayerCameraFactory playerCameraFactory,
            IProvider<LocationTypeId, Transform> locationProvider,
            IProvider<Camera> cameraProvider,
            IProvider<Player> playerProvider,
            IPlayerStorage playerStorage,
            WeaponSelector weaponSelector, 
            WalletPresenter walletPresenter,
            ISaveSystem saveSystem, 
            ShopWeaponPresenter shopWeaponPresenter,
            IProvider<List<EnemySpawner>> enemySpawnersProvider, 
            SlowMotionOnEnemyDeath slowMotionOnEnemyDeath,
            CountEnemiesOnDeath countEnemiesOnDeath, 
            LevelSliderPresenter levelSliderPresenter, 
            WaterSplashPoolInitializer waterSplashPoolInitializer, 
            CameraShakeMediator cameraShakeMediator, RotateCameraPresenter rotateCameraPresenter)
        {
            _rotateCameraPresenter = rotateCameraPresenter;
            _cameraShakeMediator = cameraShakeMediator;
            _waterSplashPoolInitializer = waterSplashPoolInitializer;
            _levelSliderPresenter = levelSliderPresenter;
            _countEnemiesOnDeath = countEnemiesOnDeath;
            _slowMotionOnEnemyDeath = slowMotionOnEnemyDeath;
            _enemySpawners = enemySpawnersProvider.Get();
            _shopWeaponPresenter = shopWeaponPresenter;
            _saveSystem = saveSystem;
            _walletPresenter = walletPresenter;
            _weaponSelector = weaponSelector;
            _playerCameraFactory = playerCameraFactory;
            _locationProvider = locationProvider;
            _cameraProvider = cameraProvider;
            _playerProvider = playerProvider;
            _playerStorage = playerStorage;
        }

        public async void Initialize()
        {
            _enemySpawners.ForEach(x => x.Init((enemy, aggrozone) =>
            {
                _enemyConfigurator.Configure(enemy,aggrozone);
                _slowMotionOnEnemyDeath.Init(enemy);
                _countEnemiesOnDeath.Init(enemy);
                _levelSliderPresenter.Init(enemy);
                _cameraShakeMediator.InitEnemies(enemy);
            }));
            
            var playerData = await _saveSystem.Load<PlayerData>();
            _walletPresenter.Init(playerData.Money);
            _shopWeaponPresenter.Init(WeaponTypeId.GreenWeapon);
            
            InitializeInitialWeapon(WeaponTypeId.ThrowingKnifeShooter);
            Player player = InitializeInitialPlayer(PlayerTypeId.Wolverine);
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            var rotateCamera = playerCameraFollower.GetComponent<RotateCamera>();
            _rotateCameraPresenter.Init(rotateCamera, player);
            
            _cameraShakeMediator.SetCamerShake(playerCameraFollower.GetComponent<CameraShake>());
            _cameraShakeMediator.Init();
            _waterSplashPoolInitializer.Init();
        }

        private void InitializeInitialWeapon(WeaponTypeId weaponTypeId)
        {
            _weaponSelector.SetLastWeaponChoosed(weaponTypeId);
            _weaponSelector.Select();
        }

        private PlayerCameraFollower InitializePlayerCamera()
        {
            PlayerCameraFollower playerCamera = _playerCameraFactory
                .Create(_locationProvider.Get(LocationTypeId.CameraSpawnPoint).position);
            var newCameraProvider = _cameraProvider as IProvider<CameraData>;
            newCameraProvider.Set(playerCamera.GetComponent<CameraData>());
            _cameraProvider.Set(playerCamera.GetComponent<CameraData>().Camera);
            return playerCamera;
        }

        private Player InitializeInitialPlayer(PlayerTypeId playerTypeId) =>
            _playerStorage.GetById(playerTypeId);
    }
}