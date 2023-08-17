using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Gameplay.Loots;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class LootFactory
    {
        private readonly DiContainer _diContainer;
        private readonly List<LootData> _lootDatas;
        private readonly IProvider<LocationTypeId, Transform> _locationProvider;

        public LootFactory(DiContainer diContainer, IProvider<LocationTypeId, Transform> locationProvider)
        {
            _locationProvider = locationProvider;
            _diContainer = diContainer;
            _lootDatas = Resources.LoadAll<LootData>("Prefabs/LootData")
                .ToList();
        }

        public void CreateAll(Action<Dictionary<LootTypeId, GameObject>> lootsCreator)
        {
            var loots = new Dictionary<LootTypeId, GameObject>();
        
            foreach (var lootData in _lootDatas)
            {
                var loot = _diContainer
                    .InstantiatePrefab(lootData.Prefab,
                        _locationProvider.Get(LocationTypeId.LootParent));
            
                loots[lootData.LootTypeId] = loot;
            }

            lootsCreator?.Invoke(loots);
        }
    }
}