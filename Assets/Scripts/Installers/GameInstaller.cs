using Constants;
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
            BindCameraFactory();
            BindPlayerFactory();
            BindGameFactory();
        }

        private void BindGameFactory()
        {
            Container
                .Bind<GameFactory>()
                .AsSingle();
        }

        private void BindPlayerFactory()
        {
            var playerPrefab = _assetProvider.GetAsset<Player>(AssetPath.Spiderman);
        
            Container
                .BindFactory<Vector3, Player, Player.Factory>()
                .FromComponentInNewPrefab(playerPrefab);
        }

        private void BindCameraFactory()
        {
            var cameraFollowerPrefab = _assetProvider.GetAsset<PlayerCameraFollower>(AssetPath.MainCamera);
        
            Container
                .BindFactory<Player, PlayerCameraFollower, PlayerCameraFollower.Factory>()
                .FromComponentInNewPrefab(cameraFollowerPrefab);
        }

        private void BindGameInit()
        {
            Container
                .BindInterfacesAndSelfTo<GameInit.GameInit>()
                .AsSingle();
        }

        private void BindLocationProvider()
        {
            LocationProvider locationProvider = new(_spawnPoint);
            Container
                .BindInstance(locationProvider)
                .AsSingle();
        }
    }
}