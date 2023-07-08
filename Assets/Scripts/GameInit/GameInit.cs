using System;
using Enums;
using Extensions;
using Gameplay.Camera;
using Gameplay.PlayerSelection;
using Gameplay.Weapon;
using Gameplay.WeaponSelection;
using Services.Factories;
using Services.Providers;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;
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
        private readonly WeaponSelector _weaponSelector;
        private readonly WeaponsProvider _weaponsProvider;
        private PlayerCameraFollower _playerCamera;
        private Weapon _weapon;
        private Player _player;

        public GameInit(GameFactory gameFactory, 
            LocationProvider locationProvider, 
            CameraProvider cameraProvider,
            PlayerProvider playerProvider, 
            PlayerSelector playerSelector, 
            WeaponSelector weaponSelector,  
            WeaponsProvider weaponsProvider)
        {
            _gameFactory = gameFactory;
            _locationProvider = locationProvider;
            _cameraProvider = cameraProvider;
            _playerProvider = playerProvider;
            _playerSelector = playerSelector;
            _weaponSelector = weaponSelector;
            _weaponsProvider = weaponsProvider;
        }

        public void Initialize()
        {
            InitializePlayerCamera();
            _playerSelector.PlayerSelected += InitializePlayer;
            _weaponSelector.WeaponSelected += InitializeWeapon;
        }

        private void InitializeWeapon(WeaponTypeId weaponTypeId)
        {
           _weapon = _gameFactory.CreateWeapon(weaponTypeId,null);
           _weaponsProvider.CurrentWeapon = _weapon;
        }

        public void Dispose()
        {
            _playerSelector.PlayerSelected -= InitializePlayer;
            _weaponSelector.WeaponSelected -= InitializeWeapon;
        }

        private void InitializePlayerCamera()
        {
            _playerCamera =  _gameFactory.CreateCamera(_locationProvider.CameraSpawnPoint.position);
            _cameraProvider.Camera = _playerCamera.GetComponent<Camera>();
        }

        private void InitializePlayer(PlayerTypeId playerTypeId)
        {
            Player player = _gameFactory.CreatePlayer(playerTypeId, _locationProvider.PlayerSpawnPoint.position);
            _playerCamera.SetPlayer(player);
            _player = player;

            _playerProvider.CurrentPlayer = player;
        }
    }
}