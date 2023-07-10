using System;
using Enums;
using Gameplay.Camera;
using Gameplay.PlayerSelection;
using Gameplay.Weapon;
using Services.Factories;
using Services.Providers;
using Services.Storages;
using UI;
using UnityEngine;
using Zenject;
using Player = Gameplay.Character.Player.Player;

namespace GameInit
{
    public class GameInit : IInitializable
    {
        private readonly GameFactory _gameFactory;
        private readonly LocationProvider _locationProvider;
        private readonly CameraProvider _cameraProvider;
        private readonly PlayerProvider _playerProvider;
        private readonly WeaponsProvider _weaponsProvider;
        private readonly IPlayerStorage _playerStorage;
        private readonly IWeaponStorage _weaponStorage;

        public GameInit(GameFactory gameFactory,
            LocationProvider locationProvider,
            CameraProvider cameraProvider,
            PlayerProvider playerProvider,
            IWeaponStorage weaponStorage,
            WeaponsProvider weaponsProvider, IPlayerStorage playerStorage)
        {
            _gameFactory = gameFactory;
            _weaponStorage = weaponStorage;
            _locationProvider = locationProvider;
            _cameraProvider = cameraProvider;
            _playerProvider = playerProvider;
            _weaponsProvider = weaponsProvider;
            _playerStorage = playerStorage;
        }

        public void Initialize()
        {
            Player player = InitializeInitialPlayer(PlayerTypeId.Spider);
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            Weapon weapon = InitializeInitialWeapon(WeaponTypeId.WebSpiderShooter);
            weapon.transform.SetParent(player.transform);
            _playerProvider.CurrentPlayer = player;
            _weaponsProvider.CurrentWeapon = weapon;
        }

        private Weapon InitializeInitialWeapon(WeaponTypeId weaponTypeId) =>
            _weaponStorage.Get(weaponTypeId);

        private PlayerCameraFollower InitializePlayerCamera()
        {
            PlayerCameraFollower playerCamera = _gameFactory.CreateCamera(_locationProvider.CameraSpawnPoint.position);
            _cameraProvider.Camera = playerCamera.GetComponent<Camera>();
            return playerCamera;
        }

        private Player InitializeInitialPlayer(PlayerTypeId playerTypeId) =>
            _playerStorage.Get(playerTypeId);
    }
}