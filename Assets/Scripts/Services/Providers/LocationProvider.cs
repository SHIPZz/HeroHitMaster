using UnityEngine;

namespace Services.Providers
{
    public struct LocationProvider
    {
        public Transform PlayerSpawnPoint { get; }
        public Transform CameraSpawnPoint { get; }
        public Transform PlayerParentTransform { get; }
        public Transform WeaponIconParentTransform { get; }

        public LocationProvider(Transform playerParentTransform, Transform playerSpawnPoint, Transform cameraSpawnPoint,
            Transform weaponIconParentTransform)
        {
            WeaponIconParentTransform = weaponIconParentTransform;
            PlayerParentTransform = playerParentTransform;
            PlayerSpawnPoint = playerSpawnPoint;
            CameraSpawnPoint = cameraSpawnPoint;
        }
    }
}