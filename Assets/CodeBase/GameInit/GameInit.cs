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
        private readonly ILoadingCurtain _loadingCurtain;
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

        public GameInit(PlayerCameraFactory playerCameraFactory,
            IProvider<LocationTypeId, Transform> locationProvider,
            IProvider<Camera> cameraProvider,
            IProvider<Player> playerProvider,
            IPlayerStorage playerStorage,
            WeaponSelector weaponSelector, 
            ILoadingCurtain loadingCurtain, 
            WalletPresenter walletPresenter,
            ISaveSystem saveSystem, 
            ShopWeaponPresenter shopWeaponPresenter,
            IProvider<List<EnemySpawner>> enemySpawnersProvider, 
            SlowMotionOnEnemyDeath slowMotionOnEnemyDeath,
            CountEnemiesOnDeath countEnemiesOnDeath, 
            LevelSliderPresenter levelSliderPresenter, 
            WaterSplashPoolInitializer waterSplashPoolInitializer, CameraShakeMediator cameraShakeMediator)
        {
            _cameraShakeMediator = cameraShakeMediator;
            _waterSplashPoolInitializer = waterSplashPoolInitializer;
            _levelSliderPresenter = levelSliderPresenter;
            _countEnemiesOnDeath = countEnemiesOnDeath;
            _slowMotionOnEnemyDeath = slowMotionOnEnemyDeath;
            _enemySpawners = enemySpawnersProvider.Get();
            _shopWeaponPresenter = shopWeaponPresenter;
            _saveSystem = saveSystem;
            _walletPresenter = walletPresenter;
            _loadingCurtain = loadingCurtain;
            _weaponSelector = weaponSelector;
            _playerCameraFactory = playerCameraFactory;
            _locationProvider = locationProvider;
            _cameraProvider = cameraProvider;
            _playerProvider = playerProvider;
            _playerStorage = playerStorage;
        }

        public async void Initialize()
        {
            _loadingCurtain.Show();
            
            _enemySpawners.ForEach(x => x.Init((enemy, aggrozne) =>
            {
                _enemyConfigurator.Configure(enemy,aggrozne);
                _slowMotionOnEnemyDeath.Init(enemy);
                _countEnemiesOnDeath.Init(enemy);
                _levelSliderPresenter.Init(enemy);
                _cameraShakeMediator.InitEnemies(enemy);
            }));
            
            PlayerPrefs.DeleteAll();
            var playerData = await _saveSystem.Load<PlayerData>();
            playerData.Money = 2000;
            _saveSystem.Save(playerData);
            _walletPresenter.Init(playerData.Money);
            _shopWeaponPresenter.Init(WeaponTypeId.ThrowMaceShooter);
            
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            Player player = InitializeInitialPlayer(PlayerTypeId.Wizard);
            playerCameraFollower.GetComponent<RotateCameraPresenter>().Init(player.GetComponent<PlayerHealth>());
            
            InitializeInitialWeapon(WeaponTypeId.FireBallShooter);
            _cameraShakeMediator.SetCamerShake(playerCameraFollower.GetComponent<CameraShake>());
            _cameraShakeMediator.Init();
            _waterSplashPoolInitializer.Init();
            _playerProvider.Set(player);
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