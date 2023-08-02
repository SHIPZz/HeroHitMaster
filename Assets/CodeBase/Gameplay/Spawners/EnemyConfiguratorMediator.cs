using System;
using System.Collections.Generic;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages;
using Zenject;

namespace CodeBase.Gameplay.Spawners
{
    public class EnemyConfiguratorMediator : IInitializable, IDisposable
    {
        private readonly List<EnemySpawner> _enemySpawners;
        private readonly EnemyConfigurator _enemyConfigurator;

        [Inject]
        public EnemyConfiguratorMediator(EnemySpawnersProvider enemySpawnersProvider, EnemyConfigurator enemyConfigurator)
        {
            _enemySpawners = enemySpawnersProvider.EnemySpawners;
            _enemyConfigurator = enemyConfigurator;
        }

        public void Initialize()
        {
            foreach (var enemySpawner in _enemySpawners)
            {
                enemySpawner.Init((enemy, aggrozone) => _enemyConfigurator.Configure(enemy, aggrozone));
                // enemySpawner.Spawned += _enemyConfigurator.Configure;
            }
        }

        public void Dispose()
        {
            // foreach (var enemySpawner in _enemySpawners)
            // {
            //     enemySpawner.Spawned += _enemyConfigurator.Configure;
            // }
            
            // _enemySpawners.ForEach(x => x.Spawned -= _enemyConfigurator.Configure);
        }
    }
}