using System.Collections.Generic;
using CodeBase.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Installers.ScriptableObjects.PlayerCamera
{
    [CreateAssetMenu(fileName = "PlayerCameraSettings", menuName = "Gameplay/PlayerCameraSettings")]
    public class PlayerCameraSettings : SerializedScriptableObject
    {
        [OdinSerialize] public Dictionary<PlayerTypeId, Vector3> CameraPlayerPositions;
    }
}