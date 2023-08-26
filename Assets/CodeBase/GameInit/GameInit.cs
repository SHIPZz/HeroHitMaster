using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Spawners;
using CodeBase.Infrastructure;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.Slowmotion;
using CodeBase.Services.Storages.Character;
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
        private ShopWeaponPresenter _shopWeaponPresenter;
        private readonly List<EnemySpawner> _enemySpawners;
        private readonly EnemyConfigurator _enemyConfigurator = new();
        private SlowMotionOnEnemyDeath _slowMotionOnEnemyDeath;

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
            IProvider<List<EnemySpawner>> enemySpawnersProvider, SlowMotionOnEnemyDeath slowMotionOnEnemyDeath)
        {
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
            }));
            
            PlayerPrefs.DeleteAll();
            var playerData = await _saveSystem.Load<PlayerData>();
            playerData.Money = 2000;
            _saveSystem.Save(playerData);
            _walletPresenter.Init(playerData.Money);
            _shopWeaponPresenter.Init(WeaponTypeId.ThrowingKnifeShooter);
            
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            Player player = InitializeInitialPlayer(PlayerTypeId.Wolverine);
            playerCameraFollower.GetComponent<RotateCameraPresenter>().Init(player.GetComponent<PlayerHealth>());
            
            InitializeInitialWeapon(WeaponTypeId.ThrowSkewerShooter);
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
            _cameraProvider.Set(playerCamera.GetComponent<Camera>());
            return playerCamera;
        }

        private Player InitializeInitialPlayer(PlayerTypeId playerTypeId) =>
            _playerStorage.GetById(playerTypeId);
    }
}