using System.Collections.Generic;
using CodeBase.Enums;
using Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ScriptableObjects.PlayerSettings
{
    [CreateAssetMenu(menuName = "Gameplay/PlayerSettings", fileName = "PlayerSettings")]
    public class PlayerSettings : SerializedScriptableObject
    {
        [field: SerializeField] public List<PlayerTypeId> PlayerTypeIds { get; private set; }
        [OdinSerialize] public Dictionary<PlayerTypeId, PlayerWithWeaponInHandTypeId> PlayerWithWeaponInHands { get; private set; }
    }
}