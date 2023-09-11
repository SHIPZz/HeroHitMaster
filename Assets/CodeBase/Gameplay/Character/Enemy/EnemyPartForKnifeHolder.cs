using CodeBase.Gameplay.MaterialChanger;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyPartForKnifeHolder : SerializedMonoBehaviour
    { 
        [field: OdinSerialize] public IDamageable Damageable { get; private set; }
        [field: OdinSerialize] public IMaterialChanger MaterialChanger { get; private set; }
        [field: SerializeField] public Enemy Enemy { get; private set; }
    }
}