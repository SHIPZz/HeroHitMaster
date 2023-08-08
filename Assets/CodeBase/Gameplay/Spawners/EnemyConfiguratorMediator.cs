using System;
using System.Collections.Generic;
using CodeBase.Services.Providers;
using Zenject;

namespace CodeBase.Gameplay.Spawners
{
    public class EnemyConfiguratorMediator : IInitializable, IDisposable
    {
        private readonly List<EnemySpawner> _enemySpawners;
        private readonly EnemyConfigurator _enemyConfigurator;

        public EnemyConfiguratorMediator(EnemySpawnersProvider enemySpawnersProvider,
            EnemyConfigurator enemyConfigurator)
        {
            _enemySpawners = enemySpawnersProvider.EnemySpawners;
            _enemyConfigurator = enemyConfigurator;
        }

        public void Initialize()
        {
            foreach (var enemySpawner in _enemySpawners)
            {
                enemySpawner.Spawned += _enemyConfigurator.Configure;
            }
        }

        public void Dispose()
        {
            foreach (var enemySpawner in _enemySpawners)
            {
                enemySpawner.Spawned -= _enemyConfigurator.Configure;
            }
        }
    }
}