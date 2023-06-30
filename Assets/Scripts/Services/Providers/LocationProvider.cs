using UnityEngine;

namespace Services.Providers
{
    public struct LocationProvider
    {
        public Transform PlayerSpawnPoint { get; }
        public Transform CameraSpawnPoint { get; }

        public LocationProvider(Transform playerSpawnPoint, Transform cameraSpawnPoint)
        {
            PlayerSpawnPoint = playerSpawnPoint;
            CameraSpawnPoint = cameraSpawnPoint;
        }
    }
}