using CodeBase.Enums;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Sound;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages;
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
        private readonly WeaponProvider _weaponProvider;
        private readonly IPlayerStorage _playerStorage;
        private readonly IWeaponStorage _weaponStorage;
        private readonly SoundWeaponChanger _soundWeaponChanger;
        private readonly WeaponSelector _weaponSelector;

        public GameInit(GameFactory gameFactory,
            LocationProvider locationProvider,
            CameraProvider cameraProvider,
            PlayerProvider playerProvider,
            IWeaponStorage weaponStorage,
            WeaponProvider weaponProvider, IPlayerStorage playerStorage,
            SoundWeaponChanger soundWeaponChanger, WeaponSelector weaponSelector)
        {
            _weaponSelector = weaponSelector;
            _soundWeaponChanger = soundWeaponChanger;
            _gameFactory = gameFactory;
            _weaponStorage = weaponStorage;
            _locationProvider = locationProvider;
            _cameraProvider = cameraProvider;
            _playerProvider = playerProvider;
            _weaponProvider = weaponProvider;
            _playerStorage = playerStorage;
        }

        public void Initialize()
        {
            Player player = InitializeInitialPlayer(PlayerTypeId.Batman);
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            InitializeInitialWeapon(WeaponTypeId.FreezeWeapon);
            _playerProvider.CurrentPlayer = player;
            InitStartWeaponSoundBy(_weaponProvider.CurrentWeapon.WeaponTypeId);
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

        private void InitStartWeaponSoundBy(WeaponTypeId weaponTypeId) =>
            _soundWeaponChanger.SetCurrentSound(weaponTypeId);
    }
}