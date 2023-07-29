using System.Collections.Generic;
using CodeBase.Gameplay.Spawners;
using UnityEngine;

namespace CodeBase.Services.Storages
{
    public class EnemySpawnersProvider : MonoBehaviour
    {
        [field: SerializeField] public List<EnemySpawner> EnemySpawners { get; private set; }
    }
}