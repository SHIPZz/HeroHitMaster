using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class LocationProvider : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public Transform CameraSpawnPoint { get; private set; }
        [field: SerializeField] public Transform SoundsParent { get; private set; }
        [field: SerializeField] public Transform EnemyParent { get; private set; }
        [field: SerializeField] public Transform WeaponsParent { get; private set; }
        [field: SerializeField] public Transform BulletParent { get; private set; }
        [field: SerializeField] public Transform DeathEffectParent { get; private set; }
    }
}