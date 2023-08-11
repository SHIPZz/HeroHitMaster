using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.EffectPlaying;
using CodeBase.Gameplay.EnemyBodyParts;
using CodeBase.Gameplay.Spawners;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Character;
using CodeBase.UI.Slider;
using CodeBase.UI.Weapons;
using CodeBase.UI.Windows.Loading;
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
        private readonly IProvider<List<EnemySpawner>> _enemySpawnersProvider;
        private readonly IEnemyStorage _enemyStorage;
        private readonly EnemiesDeathEffectOnDestruction _enemiesDeathEffectOnDestruction;
        private readonly ActivateEnemiesMovementOnFire _activateEnemiesMovementOnFire;
        private EnemyBodyPartMediator _enemyBodyPartMediator;

        public GameInit(PlayerCameraFactory playerCameraFactory,
            IProvider<LocationTypeId, Transform> locationProvider,
            IProvider<Camera> cameraProvider,
            IProvider<Player> playerProvider,
            IPlayerStorage playerStorage,
            WeaponSelector weaponSelector,
            IProvider<List<EnemySpawner>> enemySpawnersProvider,
            IEnemyStorage enemyStorage,
            EnemiesDeathEffectOnDestruction enemiesDeathEffectOnDestruction,
            ActivateEnemiesMovementOnFire activateEnemiesMovementOnFire,
            EnemyBodyPartMediator enemyBodyPartMediator)
        {
            _enemyBodyPartMediator = enemyBodyPartMediator;
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
            _playerProvider.Set(player);
        }

        private void InitEnemies(List<Enemy> enemies)
        {
            _enemiesDeathEffectOnDestruction.Init(enemies);
            _activateEnemiesMovementOnFire.Init(enemies);
            _enemySpawnersProvider.Get().ForEach(x => x.Init(_enemyStorage));
            _enemyBodyPartMediator.Init(enemies);
        }

        private void InitializeInitialWeapon(WeaponTypeId weaponTypeId) =>
            _weaponSelector.Select(weaponTypeId);

        private PlayerCameraFollower InitializePlayerCamera()
        {
            PlayerCameraFollower playerCamera = _playerCameraFactory
                .Create(_locationProvider.Get(LocationTypeId.CameraSpawnPoint).position);
            _cameraProvider.Set(playerCamera.GetComponent<Camera>());
            return playerCamera;
        }

        private Player InitializeInitialPlayer(PlayerTypeId playerTypeId) =>
            _playerStorage.GetById(playerTypeId);
    }
}