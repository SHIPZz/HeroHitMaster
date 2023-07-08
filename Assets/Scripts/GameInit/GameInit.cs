using System;
using Enums;
using Gameplay.Camera;
using Gameplay.PlayerSelection;
using Gameplay.Weapon;
using Services.Factories;
using Services.GameObjectsPoolAccess;
using Services.Providers;
using UnityEngine;
using Zenject;
using Player = Gameplay.Character.Player.Player;

namespace GameInit
{
    public class GameInit : IInitializable, IDisposable
    {
        private readonly GameFactory _gameFactory;
        private readonly LocationProvider _locationProvider;
        private readonly CameraProvider _cameraProvider;
        private readonly PlayerProvider _playerProvider;
        private readonly PlayerSelector _playerSelector;
        private readonly WeaponsProvider _weaponsProvider;
        private readonly PlayerStorage _playerStorage;

        public GameInit(GameFactory gameFactory,
            LocationProvider locationProvider,
            CameraProvider cameraProvider,
            PlayerProvider playerProvider,
            PlayerSelector playerSelector,
            WeaponsProvider weaponsProvider, PlayerStorage playerStorage)
        {
            _gameFactory = gameFactory;
            _locationProvider = locationProvider;
            _cameraProvider = cameraProvider;
            _playerProvider = playerProvider;
            _playerSelector = playerSelector;
            _weaponsProvider = weaponsProvider;
            _playerStorage = playerStorage;
        }

        public void Initialize()
        {
            Player player = InitializeInitialPlayer(PlayerTypeId.Spider);
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            Weapon weapon = InitializeInitialWeapon(WeaponTypeId.WebSpiderShooter, player.transform);
            _playerProvider.CurrentPlayer = player;
            _weaponsProvider.CurrentWeapon = weapon;
        }

        public void Dispose() { }

        private Weapon InitializeInitialWeapon(WeaponTypeId weaponTypeId, Transform parent) =>
            _gameFactory.CreateWeapon(weaponTypeId, parent);

        private PlayerCameraFollower InitializePlayerCamera()
        {
            PlayerCameraFollower playerCamera = _gameFactory.CreateCamera(_locationProvider.CameraSpawnPoint.position);
            _cameraProvider.Camera = playerCamera.GetComponent<Camera>();
            return playerCamera;
        }

        private Player InitializeInitialPlayer(PlayerTypeId playerTypeId) =>
            _playerStorage.Get(playerTypeId);

        // private Player InitializeInitialPlayer(PlayerTypeId playerTypeId) =>
        //     _gameFactory.CreatePlayer(playerTypeId, _locationProvider.PlayerSpawnPoint.position);
    }
}