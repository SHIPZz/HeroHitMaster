using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public class CameraData : MonoBehaviour
    {
        [field: SerializeField] public Transform Rotator { get; private set; }
        [field: SerializeField] public UnityEngine.Camera Camera { get; private set; }
    }
}