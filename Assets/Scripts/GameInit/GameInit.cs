using Services.Providers;
using Zenject;

namespace GameInit
{
    public class GameInit : IInitializable
    {
        private readonly GameFactory _gameFactory;
        private readonly LocationProvider _locationProvider;

        public GameInit(GameFactory gameFactory, LocationProvider locationProvider)
        {
            _gameFactory = gameFactory;
            _locationProvider = locationProvider;
        }

        public void Initialize()
        {
            Player player = CreatePlayer();
            CreateCamera(player);
        }

        private void CreateCamera(Player player) =>
            _gameFactory.CreateCameraFollower(player);

        private Player CreatePlayer() =>
            _gameFactory.CreatePlayer(_locationProvider.PlayerSpawnPoint.position);
    }
}