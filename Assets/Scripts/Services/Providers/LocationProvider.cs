using UnityEngine;

namespace Services.Providers
{
    public struct LocationProvider
    {
        public Transform PlayerSpawnPoint { get; }

        public LocationProvider(Transform playerSpawnPoint)
        {
            PlayerSpawnPoint = playerSpawnPoint;
        }
    }
}