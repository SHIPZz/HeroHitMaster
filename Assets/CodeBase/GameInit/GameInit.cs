using System.Collections.Generic;
using CodeBase.Constants;
using CodeBase.Enums;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.EffectPlaying;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.Services.Providers.AssetProviders;
using CodeBase.Services.Storages.Character;
using CodeBase.UI;
using CodeBase.UI.Slider;
using CodeBase.UI.Weapons;
using CodeBase.UI.Windows.Loading;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Player = CodeBase.Gameplay.Character.Players.Player;

namespace CodeBase.GameInit
{
    public class GameInit : IInitializable
    {
        private readonly PlayerCameraFactory _playerCameraFactory;
        private readonly LocationProvider _locationProvider;
        private readonly CameraProvider _cameraProvider;
        private readonly PlayerProvider _playerProvider;
        private readonly IPlayerStorage _playerStorage;
        private readonly WeaponSelector _weaponSelector;
        private readonly EnemySpawnersProvider _enemySpawnersProvider;
        private readonly IEnemyStorage _enemyStorage;
        private readonly EnemiesDeathEffectOnDestruction _enemiesDeathEffectOnDestruction;
        private readonly ActivateEnemiesMovementOnFire _activateEnemiesMovementOnFire;
        private SliderLoading _sliderLoading;
        private LoadingWindowPresenter _loadingWindowPresenter;

        public GameInit(PlayerCameraFactory playerCameraFactory,
            LocationProvider locationProvider,
            CameraProvider cameraProvider,
            PlayerProvider playerProvider,
            IPlayerStorage playerStorage,
            WeaponSelector weaponSelector,
            EnemySpawnersProvider enemySpawnersProvider,
            IEnemyStorage enemyStorage,
            EnemiesDeathEffectOnDestruction enemiesDeathEffectOnDestruction,
            ActivateEnemiesMovementOnFire activateEnemiesMovementOnFire,
            LoadingWindowPresenter loadingWindowPresenter)
        {
            _loadingWindowPresenter = loadingWindowPresenter;
            _activateEnemiesMovementOnFire = activateEnemiesMovementOnFire;
            _enemiesDeathEffectOnDestruction = enemiesDeathEffectOnDestruction;
            _enemyStorage = enemyStorage;
            _enemySpawnersProvider = enemySpawnersProvider;
            _weaponSelector = weaponSelector;
            _playerCameraFactory = playerCameraFactory;
            _locationProvider = locationProvider;
            _cameraProvider = cameraProvider;
            _playerProvider = playerProvider;
            _playerStorage = playerStorage;
        }

        public async void Initialize()
        { 
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            await _enemyStorage.InitTask;

            List<Enemy> enemies = _enemyStorage.GetAll();
            InitEnemies(enemies);
            Player player = InitializeInitialPlayer(PlayerTypeId.Wolverine);
            InitializeInitialWeapon(WeaponTypeId.ThrowingKnifeShooter);
            _playerProvider.CurrentPlayer = player;
        }

        private void InitEnemies(List<Enemy> enemies)
        {
            _enemiesDeathEffectOnDestruction.Init(enemies);
            _activateEnemiesMovementOnFire.Init(enemies);
            _enemySpawnersProvider.EnemySpawners.ForEach(x => x.Init(_enemyStorage));
        }

        private void InitializeInitialWeapon(WeaponTypeId weaponTypeId) =>
            _weaponSelector.Select(weaponTypeId);

        private PlayerCameraFollower InitializePlayerCamera()
        {
            PlayerCameraFollower playerCamera = _playerCameraFactory
                .Create(_locationProvider.Values[LocationTypeId.CameraSpawnPoint].position);
            _cameraProvider.Camera = playerCamera.GetComponent<Camera>();
            return playerCamera;
        }

        private Player InitializeInitialPlayer(PlayerTypeId playerTypeId) =>
            _playerStorage.GetById(playerTypeId);
    }
}