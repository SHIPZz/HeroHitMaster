using System.Collections.Generic;
using CodeBase.Services.Storages;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Spawners
{
    public class EnemyConfiguratorMediator : MonoBehaviour
    {
        private  List<EnemySpawner> _enemySpawners;
        private  EnemyConfigurator _enemyConfigurator;

        [Inject]
        public void Construct(EnemySpawnersProvider enemySpawnersProvider, EnemyConfigurator enemyConfigurator)
        {
            _enemySpawners = enemySpawnersProvider.EnemySpawners;
            _enemyConfigurator = enemyConfigurator;
        }

        public void OnEnable() => 
            _enemySpawners.ForEach(x => x.Spawned += _enemyConfigurator.Configure);

        public void OnDisable() => 
            _enemySpawners.ForEach(x => x.Spawned -= _enemyConfigurator.Configure);
    }
}