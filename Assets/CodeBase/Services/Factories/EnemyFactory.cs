using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Services.Providers;
using CodeBase.Services.Providers.AssetProviders;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class EnemyFactory
    {
        private readonly DiContainer _diContainer;
        private readonly AssetProvider _assetProvider;
        private readonly Dictionary<EnemyTypeId, string> _enemies;
        private readonly LocationProvider _locationProvider;

        public EnemyFactory(DiContainer diContainer, AssetProvider assetProvider, EnemySetting enemySetting,
            LocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
            _enemies = enemySetting.EnemyPathes;
            _diContainer = diContainer;
            _assetProvider = assetProvider;
        }

        public Enemy CreateBy(EnemyTypeId enemyTypeId)
        {
            if (!_enemies.TryGetValue(enemyTypeId, out string path))
                throw new ArgumentException("Error");

            return Create(path);
        }

        private Enemy Create(string value)
        {
            var enemy = _assetProvider.GetAsset(value);
            enemy.gameObject.SetActive(false);
            return _diContainer.InstantiatePrefabForComponent<Enemy>(enemy,_locationProvider.EnemyParent);
        }
    }
}