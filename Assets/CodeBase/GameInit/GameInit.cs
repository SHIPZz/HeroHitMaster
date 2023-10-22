using System.Collections.Generic;
using System.Threading.Tasks;
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
using CodeBase.UI.Windows.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Player = CodeBase.Gameplay.Character.Players.Player;

namespace CodeBase.GameInit
{
    public class GameInit : IInitializable
    {
        private readonly PlayerCameraFactory _playerCameraFactory;
        private readonly IProvider<LocationTypeId, Transform> _locationProvider;
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
        private readonly CameraShakeMediator _cameraShakeMediator;
        private readonly RotateCameraPresenter _rotateCameraPresenter;
        private readonly RotateCameraOnLastEnemyKilled _rotateCameraOnLastEnemyKilled;
        private readonly IProvider<CameraData> _cameraDataProvider;
        private readonly AudioView _audioView;
        private readonly AudioChanger _audioChanger;

        public GameInit(PlayerCameraFactory playerCameraFactory,
            IProvider<LocationTypeId, Transform> locationProvider,
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
            CameraShakeMediator cameraShakeMediator,
            RotateCameraPresenter rotateCameraPresenter,
            RotateCameraOnLastEnemyKilled rotateCameraOnLastEnemyKilled, 
            IProvider<CameraData> cameraDataProvider, 
            AudioView audioView, 
            AudioChanger audioChanger)
        {
            _audioChanger = audioChanger;
            _audioView = audioView;
            _cameraDataProvider = cameraDataProvider;
            _rotateCameraOnLastEnemyKilled = rotateCameraOnLastEnemyKilled;
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
            _playerStorage = playerStorage;
        }

        public async void Initialize()
        {
            var settingsData = await _saveSystem.Load<SettingsData>();
            _audioView.Slider.value = settingsData.Volume;
            _audioChanger.Change(settingsData.Volume);
            
            _enemySpawners.ForEach(x => x.Init((enemy, aggrozone) =>
            {
                _enemyConfigurator.Configure(enemy, aggrozone);
                _countEnemiesOnDeath.Init(enemy);
                _levelSliderPresenter.Init(enemy);
                _cameraShakeMediator.InitEnemies(enemy);
                _rotateCameraOnLastEnemyKilled.FillList(enemy);
            }));

            var playerData = await _saveSystem.Load<PlayerData>();
            InitializeUIPresenters(playerData);
            InitializeInitialWeapon(WeaponTypeId.ThrowButterfly);
            Player player = InitializeInitialPlayer(playerData.LastPlayerId);
            InitializeCamera(player);
            _waterSplashPoolInitializer.Init();
        }

        private void InitializeUIPresenters(PlayerData playerData)
        {
            _walletPresenter.Init(playerData.Money);
            _shopWeaponPresenter.Init(playerData.LastNotPopupWeaponId);
        }

        private void InitializeCamera(Player player)
        {
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            var rotateCamera = playerCameraFollower.GetComponent<RotateCamera>();
            _rotateCameraPresenter.Init(rotateCamera, player);
            _cameraShakeMediator.SetCamerShake(playerCameraFollower.GetComponent<CameraShake>());
            _cameraShakeMediator.Init();
            _rotateCameraOnLastEnemyKilled.Init(rotateCamera.GetComponent<CameraData>());
        }

        private void InitializeInitialWeapon(WeaponTypeId weaponTypeId)
        {
            _weaponSelector.SetLastWeaponChoosen(weaponTypeId);
            _weaponSelector.Select();
        }

        private PlayerCameraFollower InitializePlayerCamera()
        {
            PlayerCameraFollower playerCamera = _playerCameraFactory
                .Create(_locationProvider.Get(LocationTypeId.CameraSpawnPoint).position);
            var cameraData = playerCamera.GetComponent<CameraData>();
            _cameraDataProvider.Set(cameraData);
            return playerCamera;
        }

        private Player InitializeInitialPlayer(PlayerTypeId playerTypeId) =>
            _playerStorage.GetById(playerTypeId);
    }
}