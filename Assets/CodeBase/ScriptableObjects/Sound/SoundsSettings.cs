using System.Collections.Generic;
using CodeBase.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.ScriptableObjects.Sound
{
    [CreateAssetMenu(fileName = "SoundsSO", menuName = "Gameplay/SoundsSO")]
    public class SoundsSettings : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<EnemyTypeId, SoundTypeId> _dieEnemySounds;
        [OdinSerialize] private Dictionary<EnemyTypeId, SoundTypeId> _hitEnemySounds;

        public Dictionary<EnemyTypeId, SoundTypeId> DieEnemySounds => _dieEnemySounds;

        public Dictionary<EnemyTypeId, SoundTypeId> HitEnemySounds => _hitEnemySounds;
    }
}