using System;
using System.Collections.Generic;
using System.Linq;
using Agava.YandexGames;
using CodeBase.Enums;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Spawners;
using CodeBase.Gameplay.WaterSplash;
using CodeBase.Gameplay.Weapons;
using CodeBase.Infrastructure;
using CodeBase.ScriptableObjects.PlayerSettings;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services;
using CodeBase.Services.AccuracyCounters;
using CodeBase.Services.Data;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.Storages.Character;
using CodeBase.Services.UI;
using CodeBase.UI.LevelSlider;
using CodeBase.UI.Wallet;
using CodeBase.UI.Weapons;
using CodeBase.UI.Windows.Shop;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.AI;
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
        private readonly List<EnemySpawner> _enemySpawners;
        private readonly EnemyConfigurator _enemyConfigurator = new();
        private readonly CountEnemiesOnDeath _countEnemiesOnDeath;
        private readonly LevelSliderPresenter _levelSliderPresenter;
        private readonly WaterSplashPoolInitializer _waterSplashPoolInitializer;
        private readonly CameraShakeMediator _cameraShakeMediator;
        private readonly RotateCameraOnLastEnemyKilled _rotateCameraOnLastEnemyKilled;
        private readonly IProvider<CameraData> _cameraDataProvider;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private readonly KillActiveEnemiesOnPlayerRecover _killActiveEnemiesOnPlayerRecover;
        private readonly PlayerSettings _playerSettings;

        private readonly Dictionary<string, Func<WeaponData, string>> _translatedWeaponNames = new()
        {
            { "ru", weaponData => weaponData.RusName },
            { "en", weaponData => weaponData.Name },
            { "tr", weaponData => weaponData.TurkishName },
        };

        private readonly SetterWeaponToPlayerHand _setterWeaponToPlayerHand;
        private readonly IWorldDataService _worldDataService;
        private readonly IEnemyProvider _enemyProvider;
        private readonly AccuracyCounter _accuracyCounter;
        private readonly EnemiesMovementInitializer _enemiesMovementInitializer;
        private UIService _uiService;


        public GameInit(PlayerCameraFactory playerCameraFactory,
            IProvider<LocationTypeId,
                Transform> locationProvider,
            IPlayerStorage playerStorage,
            WeaponSelector weaponSelector,
            IProvider<List<EnemySpawner>> enemySpawnersProvider,
            CountEnemiesOnDeath countEnemiesOnDeath,
            LevelSliderPresenter levelSliderPresenter,
            WaterSplashPoolInitializer waterSplashPoolInitializer,
            CameraShakeMediator cameraShakeMediator,
            RotateCameraOnLastEnemyKilled rotateCameraOnLastEnemyKilled,
            IProvider<CameraData> cameraDataProvider,
            WeaponStaticDataService weaponStaticDataService,
            KillActiveEnemiesOnPlayerRecover killActiveEnemiesOnPlayerRecover,
            PlayerSettings playerSettings,
            SetterWeaponToPlayerHand setterWeaponToPlayerHand,
            IWorldDataService worldDataService,
            IEnemyProvider enemyProvider,
            AccuracyCounter accuracyCounter,
            EnemiesMovementInitializer enemiesMovementInitializer,
            UIService uiService,
            ILoadingCurtain loadingCurtain)
        {
            _uiService = uiService;
            _enemiesMovementInitializer = enemiesMovementInitializer;
            _accuracyCounter = accuracyCounter;
            _enemyProvider = enemyProvider;
            _worldDataService = worldDataService;
            _setterWeaponToPlayerHand = setterWeaponToPlayerHand;
            _playerSettings = playerSettings;
            _killActiveEnemiesOnPlayerRecover = killActiveEnemiesOnPlayerRecover;
            _weaponStaticDataService = weaponStaticDataService;
            _cameraDataProvider = cameraDataProvider;
            _rotateCameraOnLastEnemyKilled = rotateCameraOnLastEnemyKilled;
            _cameraShakeMediator = cameraShakeMediator;
            _waterSplashPoolInitializer = waterSplashPoolInitializer;
            _levelSliderPresenter = levelSliderPresenter;
            _countEnemiesOnDeath = countEnemiesOnDeath;
            _enemySpawners = enemySpawnersProvider.Get();
            _weaponSelector = weaponSelector;
            _playerCameraFactory = playerCameraFactory;
            _locationProvider = locationProvider;
            _playerStorage = playerStorage;
        }

        public async void Initialize()
        {
            LocalizationManager.CurrentLanguage = YandexGamesSdk.Environment.i18n.lang;
            YandexGamesSdk.GameReady();
            YandexGamesSdk.CallbackLogging = false;

            WorldData worldData = _worldDataService.WorldData;
            TranslateWeaponNames(worldData);
            InitEnemiesAndObjectsWhoNeedEnemies();
            InitPlayerBeforeWeaponChoose(worldData.PlayerData);
            InitUIService(worldData);
            Weapon weapon = await InitializeInitialWeapon(worldData.PlayerData.LastWeaponId);
            InitializeCamera(weapon); 
            InitPlayerAfterWeaponChoose(weapon);
            InitAnotherServices(weapon);
            InitMainUI();
        }

        private void InitAnotherServices(Weapon weapon)
        {
            _waterSplashPoolInitializer.Init();
            _setterWeaponToPlayerHand.Init(weapon.WeaponTypeId);
        }

        private void InitUIService(WorldData worldData) =>
            _uiService.Init(worldData);

        private void InitMainUI()
        {
            _uiService.EnableMainUI();
        }

        private Player InitPlayerAfterWeaponChoose(Weapon weapon)
        {
            PlayerTypeId targetPlayerId = _playerSettings.PlayerTypeIdsByWeapon[weapon.WeaponTypeId];
            Player targetPlayer = GetPlayerFromStorage(targetPlayerId);

            return targetPlayer;
        }

        private void InitPlayerBeforeWeaponChoose(PlayerData playerData)
        {
            PlayerTypeId initialPlayerId = _playerSettings.PlayerTypeIdsByWeapon[playerData.LastWeaponId];
            GetPlayerFromStorage(initialPlayerId);
        }

        private void InitEnemiesAndObjectsWhoNeedEnemies()
        {
            _enemySpawners.ForEach(x => x.Init((enemy, aggrozone) =>
            {
                _enemyProvider.Enemies.Add(enemy);
                _enemyConfigurator.Configure(enemy, aggrozone);
            }));

            _enemiesMovementInitializer.Init();
            _countEnemiesOnDeath.Init(_enemyProvider.Enemies);
            _levelSliderPresenter.Init(_enemyProvider.Enemies);
            _killActiveEnemiesOnPlayerRecover.Init(_enemyProvider.Enemies);
            _cameraShakeMediator.InitEnemies(_enemyProvider.Enemies);
            _rotateCameraOnLastEnemyKilled.Init(_enemyProvider.Enemies);
            _accuracyCounter.Init(_enemyProvider.Enemies);
        }

        private void TranslateWeaponNames(WorldData worldData)
        {
            List<WeaponData> weaponDatas = _weaponStaticDataService.GetAll();

            if (!_translatedWeaponNames.TryGetValue(LocalizationManager.CurrentLanguage,
                    out Func<WeaponData, string> namePropertyGetter))
                return;

            foreach (WeaponData weaponData in weaponDatas)
            {
                worldData.TranslatedWeaponNameData.Names[weaponData.WeaponTypeId] =
                    namePropertyGetter?.Invoke(weaponData);
            }

            _worldDataService.Save();
        }

        private void InitializeCamera(Weapon weapon)
        {
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            _cameraShakeMediator.SetCamerShake(playerCameraFollower.GetComponent<CameraShake>());
            _cameraShakeMediator.Init(weapon);
            _rotateCameraOnLastEnemyKilled.Init(playerCameraFollower.GetComponent<CameraData>());
        }

        private async UniTask<Weapon> InitializeInitialWeapon(WeaponTypeId weaponTypeId)
        {
            Weapon weapon = _weaponSelector.Init(weaponTypeId);

            while (weapon.BulletsCreated == false)
            {
                await UniTask.Yield();
            }

            return weapon;
        }

        private PlayerCameraFollower InitializePlayerCamera()
        {
            PlayerCameraFollower playerCamera = _playerCameraFactory
                .Create(_locationProvider.Get(LocationTypeId.CameraSpawnPoint).position);
            var cameraData = playerCamera.GetComponent<CameraData>();
            _cameraDataProvider.Set(cameraData);
            return playerCamera;
        }

        private Player GetPlayerFromStorage(PlayerTypeId playerTypeId) =>
            _playerStorage.GetById(playerTypeId);
    }
}