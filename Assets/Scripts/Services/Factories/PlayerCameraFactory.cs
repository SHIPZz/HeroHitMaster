using Constants;
using Gameplay.Camera;
using Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace Services.Factories
{
    public class PlayerCameraFactory
    {
        private readonly AssetProvider _assetProvider;
        private DiContainer _diContainer;

        public PlayerCameraFactory(AssetProvider assetProvider, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
        }

        public PlayerCameraFollower Create(Vector3 at)
        {
            var camera = _assetProvider.GetAsset(AssetPath.MainCamera);
            return _diContainer.InstantiatePrefabForComponent<PlayerCameraFollower>(camera,at,Quaternion.identity, null);
        }
    }
}