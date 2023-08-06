using CodeBase.Enums;
using CodeBase.Gameplay.Camera;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Character;
using CodeBase.Services.Storages.Weapon;
using CodeBase.UI.Weapons;
using UnityEngine;
using Zenject;
using Player = CodeBase.Gameplay.Character.Players.Player;

namespace CodeBase.GameInit
{
    public class GameInit : IInitializable
    {
        private readonly GameFactory _gameFactory;
        private readonly LocationProvider _locationProvider;
        private readonly CameraProvider _cameraProvider;
        private readonly PlayerProvider _playerProvider;
        private readonly IPlayerStorage _playerStorage;
        private readonly WeaponSelector _weaponSelector;

        public GameInit(GameFactory gameFactory,
            LocationProvider locationProvider,
            CameraProvider cameraProvider,
            PlayerProvider playerProvider, 
            IPlayerStorage playerStorage, WeaponSelector weaponSelector)
        {
            _weaponSelector = weaponSelector;
            _gameFactory = gameFactory;
            _locationProvider = locationProvider;
            _cameraProvider = cameraProvider;
            _playerProvider = playerProvider;
            _playerStorage = playerStorage;
        }

        public void Initialize()
        {
            Player player = InitializeInitialPlayer(PlayerTypeId.Wizard);
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            InitializeInitialWeapon(WeaponTypeId.FireBallShooter);
            _playerProvider.CurrentPlayer = player;
        }

        private void InitializeInitialWeapon(WeaponTypeId weaponTypeId) =>
            _weaponSelector.Select(weaponTypeId);

        private PlayerCameraFollower InitializePlayerCamera()
        {
            PlayerCameraFollower playerCamera = _gameFactory.CreateCamera(_locationProvider.CameraSpawnPoint.position);
            _cameraProvider.Camera = playerCamera.GetComponent<Camera>();
            return playerCamera;
        }

        private Player InitializeInitialPlayer(PlayerTypeId playerTypeId) =>
            _playerStorage.GetById(playerTypeId);
    }
}