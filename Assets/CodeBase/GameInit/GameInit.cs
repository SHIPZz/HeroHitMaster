using CodeBase.Enums;
using CodeBase.Gameplay.Camera;
using CodeBase.Infrastructure;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Character;
using CodeBase.UI.Weapons;
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
        private ILoadingCurtain _loadingCurtain;

        public GameInit(PlayerCameraFactory playerCameraFactory,
            IProvider<LocationTypeId, Transform> locationProvider,
            IProvider<Camera> cameraProvider,
            IProvider<Player> playerProvider,
            IPlayerStorage playerStorage,
            WeaponSelector weaponSelector, ILoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
            _weaponSelector = weaponSelector;
            _playerCameraFactory = playerCameraFactory;
            _locationProvider = locationProvider;
            _cameraProvider = cameraProvider;
            _playerProvider = playerProvider;
            _playerStorage = playerStorage;
        }

        public void Initialize()
        {
            _loadingCurtain.Show();
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            Player player = InitializeInitialPlayer(PlayerTypeId.Spider);
            InitializeInitialWeapon(WeaponTypeId.SmudgeWebShooter);
            _playerProvider.Set(player);
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