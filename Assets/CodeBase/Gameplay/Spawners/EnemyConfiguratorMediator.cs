using System;
using System.Collections.Generic;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Spawners
{
    public class EnemyConfiguratorMediator : MonoBehaviour
    {
        private List<EnemySpawner> _enemySpawners;
        private readonly EnemyConfigurator _enemyConfigurator = new();

        [Inject]
        public void Construct(IProvider<List<EnemySpawner>> enemySpawnersProvider)
        {
            _enemySpawners = enemySpawnersProvider.Get();
        }

        private void OnEnable()
        {
            foreach (var enemySpawner in _enemySpawners)
            {
                enemySpawner.Init((enemy,aggroZone) => _enemyConfigurator.Configure(enemy, aggroZone));
            }
        }
    }
}