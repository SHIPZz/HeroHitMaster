using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Loots
{
    public class LootStorage
    {
        private readonly Dictionary<LootTypeId, GameObject> _loots = new();
        private readonly DiContainer _diContainer;

        public LootStorage(DiContainer diContainer, LocationProvider locationProvider)
        {
            _diContainer = diContainer;
            var loots = Resources.LoadAll<LootData>("Prefabs/LootData").ToList();

            FillDictionary(locationProvider, loots);
        }

        public GameObject GetBy(LootTypeId lootTypeId) =>
            _loots[lootTypeId];

        private void FillDictionary(LocationProvider locationProvider, List<LootData> loots)
        {
            foreach (var lootData in loots)
            {
                var loot = _diContainer
                    .InstantiatePrefab(lootData.Prefab, locationProvider.Values[LocationTypeId.LootParent]);

                _loots[lootData.LootTypeId] = loot;
            }
        }
    }
}