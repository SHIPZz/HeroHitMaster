using System.Collections.Generic;
using CodeBase.Gameplay.Spawners;
using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class EnemySpawnersProvider : MonoBehaviour, IProvider<List<EnemySpawner>>
    {
        [field: SerializeField] public List<EnemySpawner> EnemySpawners { get; private set; }

        public List<EnemySpawner> Get() => 
            EnemySpawners;

        public void Set(List<EnemySpawner> t) => 
            EnemySpawners = t;
    }
}