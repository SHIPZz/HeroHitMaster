using CodeBase.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.ScriptableObjects.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Gameplay/EnemyData")]
    public class EnemyData : SerializedScriptableObject
    {
        [Range(1, 8)] public float Speed;

        [Range(100, 1000)] public int Hp;

        public int Damage = 1000;
        public EnemyTypeId EnemyTypeId;
        public GameObject Prefab;
    }
}