using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.EnemyBodyParts;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.ScriptableObjects.Enemy
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Gameplay/EnemySettings")]
    public class EnemySetting : SerializedScriptableObject
    {
        [field: OdinSerialize] public Dictionary<EnemyTypeId, string> EnemyPathes { get; private set; }
        [field: OdinSerialize] public Dictionary<EnemyTypeId, EnemyBodyPart> EnemyBodyParts { get; private set; }
        [field: SerializeField] public List<EnemyTypeId> EnemyTypeIds { get; private set; }
    }
}