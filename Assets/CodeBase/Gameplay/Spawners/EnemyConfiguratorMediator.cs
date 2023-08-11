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

        public EnemyConfiguratorMediator(IProvider<List<EnemySpawner>> enemySpawnersProvider,
            EnemyConfigurator enemyConfigurator)
        {
            _enemySpawners = enemySpawnersProvider.Get();
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