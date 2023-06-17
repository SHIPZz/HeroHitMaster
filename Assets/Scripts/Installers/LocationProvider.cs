using UnityEngine;

namespace Installers
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