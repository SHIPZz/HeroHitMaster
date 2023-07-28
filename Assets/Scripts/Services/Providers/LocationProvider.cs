using UnityEngine;

namespace Services.Providers
{
    public struct LocationProvider
    {
        public Transform PlayerSpawnPoint { get; }
        public Transform CameraSpawnPoint { get; }
        public Transform PlayerParentTransform { get; }
        public Transform WeaponIconParentTransform { get; }
        public Transform SoundsParent { get; }

        public LocationProvider(Transform playerSpawnPoint, 
            Transform cameraSpawnPoint, 
            Transform playerParentTransform, 
            Transform weaponIconParentTransform, 
            Transform soundsParent)
        {
            PlayerSpawnPoint = playerSpawnPoint;
            CameraSpawnPoint = cameraSpawnPoint;
            PlayerParentTransform = playerParentTransform;
            WeaponIconParentTransform = weaponIconParentTransform;
            SoundsParent = soundsParent;
        }
    }
}