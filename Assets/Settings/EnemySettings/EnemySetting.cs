using System.Collections.Generic;
using CodeBase.Gameplay.EnemyBodyParts;
using Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Gameplay/EnemySettings")]
public class EnemySetting : SerializedScriptableObject
{
    [field: OdinSerialize] public Dictionary<EnemyTypeId, string> EnemyPathes { get; private set; }
    [field: OdinSerialize] public Dictionary<EnemyTypeId, EnemyBodyPart> EnemyBodyParts { get; private set; }
    [field: SerializeField] public List<EnemyTypeId> EnemyTypeIds { get; private set; }
}