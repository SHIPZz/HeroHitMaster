using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Spawners;
using CodeBase.Gameplay.WaterSplash;
using CodeBase.Gameplay.Weapons;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services;
using CodeBase.Services.Data;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.Storages.Character;
using CodeBase.Services.Storages.Weapon;
using CodeBase.UI.LevelSlider;
using CodeBase.UI.Wallet;
using CodeBase.UI.Weapons;
using CodeBase.UI.Weapons.ShopWeapons;
using CodeBase.UI.Windows.Audio;
using Cysharp.Threading.Tasks;
using I2.Loc;
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
        private readonly LevelSliderPresenter _levelSliderPresenter;
        private readonly WaterSplashPoolInitializer _waterSplashPoolInitializer;
        private readonly CameraShakeMediator _cameraShakeMediator;
        private readonly RotateCameraPresenter _rotateCameraPresenter;
        private readonly RotateCameraOnLastEnemyKilled _rotateCameraOnLastEnemyKilled;
        private readonly IProvider<CameraData> _cameraDataProvider;
        private readonly AudioView _audioView;
        private readonly AudioChanger _audioChanger;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private readonly KillActiveEnemiesOnPlayerRecover _killActiveEnemiesOnPlayerRecover;
        private readonly IWeaponStorage _weaponStorage;

        private readonly Dictionary<string, Func<WeaponData, string>> _translatedWeaponNames = new()
        {
            { "ru", weaponData => weaponData.RusName },
            { "en", weaponData => weaponData.Name },
            { "tr", weaponData => weaponData.TurkishName },
        };


        public GameInit(PlayerCameraFactory playerCameraFactory,
            IProvider<LocationTypeId, Transform> locationProvider,
            IPlayerStorage playerStorage,
            WeaponSelector weaponSelector,
            WalletPresenter walletPresenter,
            ISaveSystem saveSystem,
            ShopWeaponPresenter shopWeaponPresenter,
            IProvider<List<EnemySpawner>> enemySpawnersProvider,
            CountEnemiesOnDeath countEnemiesOnDeath,
            LevelSliderPresenter levelSliderPresenter,
            WaterSplashPoolInitializer waterSplashPoolInitializer,
            CameraShakeMediator cameraShakeMediator,
            RotateCameraPresenter rotateCameraPresenter,
            RotateCameraOnLastEnemyKilled rotateCameraOnLastEnemyKilled,
            IProvider<CameraData> cameraDataProvider,
            AudioView audioView,
            AudioChanger audioChanger,
            WeaponStaticDataService weaponStaticDataService,
            KillActiveEnemiesOnPlayerRecover killActiveEnemiesOnPlayerRecover,
            IWeaponStorage weaponStorage)
        {
            _weaponStorage = weaponStorage;
            _killActiveEnemiesOnPlayerRecover = killActiveEnemiesOnPlayerRecover;
            _weaponStaticDataService = weaponStaticDataService;
            _audioChanger = audioChanger;
            _audioView = audioView;
            _cameraDataProvider = cameraDataProvider;
            _rotateCameraOnLastEnemyKilled = rotateCameraOnLastEnemyKilled;
            _rotateCameraPresenter = rotateCameraPresenter;
            _cameraShakeMediator = cameraShakeMediator;
            _waterSplashPoolInitializer = waterSplashPoolInitializer;
            _levelSliderPresenter = levelSliderPresenter;
            _countEnemiesOnDeath = countEnemiesOnDeath;
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
            LocalizationManager.CurrentLanguage = "en";
            var settingsData = await _saveSystem.Load<SettingsData>();
            
            TranslateWeaponNames();
            
            InitSound(settingsData);

            InitEnemiesAndObjectsWhoNeedEnemies();

            var playerData = await _saveSystem.Load<PlayerData>();
            Player player = InitializeInitialPlayer(playerData.LastPlayerId);
            Weapon weapon = await InitializeInitialWeapon(playerData.LastWeaponId, playerData);

            InitializeUIPresenters(playerData);
            InitializeCamera(player, weapon);
            _waterSplashPoolInitializer.Init();
        }

        private void InitSound(SettingsData settingsData)
        {
            _audioView.Slider.value = settingsData.Volume;
            _audioChanger.Change(settingsData.Volume);
        }

        private void InitEnemiesAndObjectsWhoNeedEnemies()
        {
            _enemySpawners.ForEach(x => x.Init((enemy, aggrozone) =>
            {
                _enemyConfigurator.Configure(enemy, aggrozone);
                _countEnemiesOnDeath.Init(enemy);
                _levelSliderPresenter.Init(enemy);
                _killActiveEnemiesOnPlayerRecover.Init(enemy);
                _cameraShakeMediator.InitEnemies(enemy);
                _rotateCameraOnLastEnemyKilled.FillList(enemy);
            }));
        }

        private async void TranslateWeaponNames()
        {
            var weaponNamesData = await _saveSystem.Load<TranslatedWeaponNameData>();
            List<WeaponData> weaponDatas = _weaponStaticDataService.GetAll();

            if (!_translatedWeaponNames.TryGetValue(LocalizationManager.CurrentLanguage,
                    out Func<WeaponData, string> namePropertyGetter))
                return;

            foreach (WeaponData weaponData in weaponDatas)
            {
                weaponNamesData.Names[weaponData.WeaponTypeId] = namePropertyGetter?.Invoke(weaponData);
            }
            
            _saveSystem.Save(weaponNamesData);
        }

        private void InitializeUIPresenters(PlayerData playerData)
        {
            _walletPresenter.Init(playerData.Money);
            _shopWeaponPresenter.Init(playerData.LastNotPopupWeaponId);
        }

        private void InitializeCamera(Player player, Weapon weapon)
        {
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            var rotateCamera = playerCameraFollower.GetComponent<RotateCamera>();
            _rotateCameraPresenter.Init(rotateCamera, player);
            _cameraShakeMediator.SetCamerShake(playerCameraFollower.GetComponent<CameraShake>());
            _cameraShakeMediator.Init(weapon);
            _rotateCameraOnLastEnemyKilled.Init(rotateCamera.GetComponent<CameraData>());
        }

        private async UniTask<Weapon> InitializeInitialWeapon(WeaponTypeId weaponTypeId, PlayerData playerData)
        {
            _weaponSelector.SetLastWeaponChoosen(weaponTypeId);
            _weaponSelector.Select();
            Weapon weapon = _weaponStorage.Get(playerData.LastWeaponId);
            
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

        private Player InitializeInitialPlayer(PlayerTypeId playerTypeId) =>
            _playerStorage.GetById(playerTypeId);
    }
}