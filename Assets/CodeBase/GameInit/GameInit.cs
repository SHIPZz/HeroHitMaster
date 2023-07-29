using CodeBase.Services.Providers;
using CodeBase.Services.Storages;
using Enums;
using Gameplay.Camera;
using Gameplay.Sound;
using Gameplay.Weapons;
using Services.Factories;
using Services.Providers;
using UnityEngine;
using Zenject;
using Player = Gameplay.Character.Players.Player;

namespace GameInit
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
        private SoundWeaponChanger _soundWeaponChanger;

        public GameInit(GameFactory gameFactory,
            LocationProvider locationProvider,
            CameraProvider cameraProvider,
            PlayerProvider playerProvider,
            IWeaponStorage weaponStorage,
            WeaponProvider weaponProvider, IPlayerStorage playerStorage,
            SoundWeaponChanger soundWeaponChanger)
        {
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
            Player player = InitializeInitialPlayer(PlayerTypeId.Wolverine);
            PlayerCameraFollower playerCameraFollower = InitializePlayerCamera();
            Weapon weapon = InitializeInitialWeapon(WeaponTypeId.ThrowingKnifeShooter);
            var weaponViewStorage = player.GetComponentInChildren<WeaponViewStorage>();
            weaponViewStorage.Get(weapon.WeaponTypeId);
            _playerProvider.CurrentPlayer = player;
            _weaponProvider.CurrentWeapon = weapon;
            InitStartWeaponSoundBy(weapon.WeaponTypeId);
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

        private void InitStartWeaponSoundBy(WeaponTypeId weaponTypeId) =>
            _soundWeaponChanger.SetCurrentSound(weaponTypeId);
    }
}