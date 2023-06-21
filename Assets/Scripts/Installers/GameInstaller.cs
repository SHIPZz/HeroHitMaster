using Constants;
using Gameplay.Camera;
using Gameplay.Character.Player;
using Services;
using Services.Providers;
using Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform _spawnPoint;

        private AssetProvider _assetProvider;

        [Inject]
        public void Construct(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public override void InstallBindings()
        {
            BindLocationProvider();
            BindGameInit();
            PlayerCameraFollower cameraFollowerPrefab = GetPlayerCameraFollowerPrefab();
            Player player = GetPlayerPrefab();
            BindCameraFactory(cameraFollowerPrefab);
            BindPlayerFactory(player);
            BindGameFactory();
            BindCameraProvider();
            BindPlayerProvider();
        }

        private void BindPlayerProvider() =>
            Container
                .Bind<PlayerProvider>()
                .AsSingle();

        private void BindCameraProvider() =>
            Container
                .Bind<CameraProvider>().AsSingle();

        private void BindGameFactory() =>
            Container
                .Bind<GameFactory>()
                .AsSingle();

        private void BindPlayerFactory(Player player) =>
            Container
                .BindFactory<Vector3, Player, Player.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<PlayerInstaller>(player);

        private void BindCameraFactory(PlayerCameraFollower playerCameraFollower) =>
            Container
                .BindFactory<Player, PlayerCameraFollower, PlayerCameraFollower.Factory>()
                .FromComponentInNewPrefab(playerCameraFollower);

        private PlayerCameraFollower GetPlayerCameraFollowerPrefab() =>
            _assetProvider.GetAsset<PlayerCameraFollower>(AssetPath.MainCamera);

        private void BindGameInit() =>
            Container
                .BindInterfacesAndSelfTo<GameInit.GameInit>()
                .AsSingle();

        private Player GetPlayerPrefab() =>
            _assetProvider.GetAsset<Player>(AssetPath.Spiderman);

        private void BindLocationProvider()
        {
            LocationProvider locationProvider = new(_spawnPoint);
            Container
                .BindInstance(locationProvider)
                .AsSingle();
        }
    }
}