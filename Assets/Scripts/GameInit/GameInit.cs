using Gameplay.Character.Player;
using Services;
using Services.Providers;
using UnityEngine;
using Zenject;

namespace GameInit
{
    public class GameInit : IInitializable
    {
        private readonly GameFactory _gameFactory;
        private readonly LocationProvider _locationProvider;
        private readonly CameraProvider _cameraProvider;
        private readonly PlayerProvider _playerProvider;

        public GameInit(GameFactory gameFactory, LocationProvider locationProvider, CameraProvider cameraProvider,
            PlayerProvider playerProvider)
        {
            _gameFactory = gameFactory;
            _locationProvider = locationProvider;
            _cameraProvider = cameraProvider;
            _playerProvider = playerProvider;
        }

        public void Initialize()
        {
            Player player = CreatePlayer();
            _playerProvider.Player = player;
            Camera camera =  CreateCamera(player);
            _cameraProvider.Camera = camera;
        }

        private Camera CreateCamera(Player player) =>
            _gameFactory.CreateCameraFollower(player).GetComponent<Camera>();

        private Player CreatePlayer() =>
            _gameFactory.CreatePlayer(_locationProvider.PlayerSpawnPoint.position);
    }
}