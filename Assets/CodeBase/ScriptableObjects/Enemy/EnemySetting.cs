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
        [field: SerializeField] public List<EnemyTypeId> EnemyTypeIds { get; private set; }
    }
}