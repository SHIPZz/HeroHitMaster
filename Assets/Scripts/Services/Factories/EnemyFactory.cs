using System;
using System.Collections.Generic;
using Constants;
using Enums;
using Gameplay.Character.Enemy;
using Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace Services.Factories
{
    public class EnemyFactory
    {
        private readonly DiContainer _diContainer;
        private readonly AssetProvider _assetProvider;
        private readonly Dictionary<EnemyTypeId, string> _enemies = new()
        {
            { EnemyTypeId.SnakeLet, AssetPath.SnakeLet },
            { EnemyTypeId.WolfPup, AssetPath.WolfPup },
            { EnemyTypeId.Werewolf, AssetPath.Werewolf },
            { EnemyTypeId.Dummy, AssetPath.Dummy },
            { EnemyTypeId.SnakeNaga, AssetPath.SnakeNaga },
            { EnemyTypeId.Spike, AssetPath.Spike },
            { EnemyTypeId.Sunflora, AssetPath.Sunflora },
            { EnemyTypeId.SnowBomb, AssetPath.SnowBomb },
            { EnemyTypeId.Bomb, AssetPath.Bomb },
        };

        public EnemyFactory(DiContainer diContainer, AssetProvider assetProvider)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
        }

        public Enemy CreateBy(EnemyTypeId enemyTypeId, Vector3 spawnPoint)
        {
            if (!_enemies.TryGetValue(enemyTypeId, out string path))
                throw new ArgumentException("Error");

            return Create(path,spawnPoint);
        }

        private Enemy Create(string value, Vector3 spawnPoint)
        {
            var enemy = _assetProvider.GetAsset(value);
            return _diContainer.InstantiatePrefabForComponent<Enemy>(enemy, spawnPoint, Quaternion.identity,null);
        }
    }
}