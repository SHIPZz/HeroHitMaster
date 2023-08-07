using UnityEngine;

namespace CodeBase.Gameplay.Loots
{
    [CreateAssetMenu(fileName = "LootData", menuName = "Gameplay/Loot Data")]
    public class LootData : ScriptableObject
    {
        public LootTypeId LootTypeId;
        public GameObject Prefab;
    }
}