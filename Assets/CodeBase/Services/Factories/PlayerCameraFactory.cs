using CodeBase.Constants;
using CodeBase.Gameplay.Camera;
using CodeBase.Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class PlayerCameraFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly DiContainer _diContainer;

        public PlayerCameraFactory(AssetProvider assetProvider, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
        }

        public Camera Create()
        {
            return _diContainer
                .InstantiatePrefabForComponent<Camera>(_assetProvider.GetAsset(AssetPath.MainCamera));
        }

        public PlayerCameraFollower Create(Vector3 at)
        {
            var camera = _assetProvider.GetAsset(AssetPath.MainCamera);
            return _diContainer.InstantiatePrefabForComponent<PlayerCameraFollower>(camera,at,Quaternion.identity, null);
        }
    }
}