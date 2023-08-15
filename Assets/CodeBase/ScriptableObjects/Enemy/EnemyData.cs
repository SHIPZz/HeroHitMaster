using CodeBase.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.ScriptableObjects.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Gameplay/EnemyData")]
    public class EnemyData : SerializedScriptableObject
    {
        public string Name;
        [Range(1, 8)] public float Speed;

        [Range(100, 300)] public int Hp;

        public int Damage = 1000;
        public EnemyTypeId EnemyTypeId;
        public AssetReferenceGameObject PrefabReferencee;
        public GameObject Prefab;
    }
}