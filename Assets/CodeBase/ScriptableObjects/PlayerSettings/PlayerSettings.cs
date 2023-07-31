using System.Collections.Generic;
using CodeBase.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.ScriptableObjects.PlayerSettings
{
    [CreateAssetMenu(menuName = "Gameplay/PlayerSettings", fileName = "PlayerSettings")]
    public class PlayerSettings : SerializedScriptableObject
    {
        [field: SerializeField] public List<PlayerTypeId> PlayerTypeIds { get; private set; }
        [OdinSerialize] public Dictionary<PlayerTypeId, PlayerWithWeaponInHandTypeId> PlayerWithWeaponInHands { get; private set; }
        [OdinSerialize] public Dictionary<WeaponTypeId, PlayerTypeId> PlayerTypeIdsByWeapon { get; private set; }
    }
}