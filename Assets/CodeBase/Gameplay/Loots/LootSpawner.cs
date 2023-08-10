using CodeBase.Gameplay.Character;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Loots
{
    public class LootSpawner : SerializedMonoBehaviour
    {
        [OdinSerialize] private IHealth _health;
        [SerializeField] private LootTypeId _lootTypeId;
        
        private LootStorage _lootStorage;

        [Inject]
        private void Construct(LootStorage lootStorage) => 
            _lootStorage = lootStorage;

        private void OnEnable() => 
            _health.ValueZeroReached += Spawn;

        private void OnDisable() => 
            _health.ValueZeroReached -= Spawn;

        private void Spawn()
        {
            var loot = _lootStorage.GetBy(_lootTypeId);
            loot.transform.position = _health.GameObject.transform.position;
        }
    }
}