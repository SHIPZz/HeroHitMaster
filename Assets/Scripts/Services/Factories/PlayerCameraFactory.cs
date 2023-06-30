using Constants;
using Gameplay.Camera;
using Services.Providers.AssetProviders;
using UnityEngine;

namespace Services.Factories
{
    public class PlayerCameraFactory
    {
        private AssetProvider _assetProvider;

        public PlayerCameraFactory(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public PlayerCameraFollower Create(Vector3 at)
        {
            PlayerCameraFollower camera = _assetProvider.GetAsset<PlayerCameraFollower>(AssetPath.MainCamera);
            return Object.Instantiate(camera,at,Quaternion.identity);
        }
    }
}