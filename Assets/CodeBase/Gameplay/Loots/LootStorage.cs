using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Gameplay.Loots
{
    public class LootStorage
    {
        private Dictionary<LootTypeId, GameObject> _loots = new();

        public LootStorage(LootFactory lootFactory) => 
        lootFactory.CreateAll(x => _loots = x);

        public GameObject GetBy(LootTypeId lootTypeId) =>
            _loots[lootTypeId];
    }
}