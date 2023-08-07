using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class EnemyFactory
    {
        private readonly DiContainer _diContainer;
        private readonly Dictionary<EnemyTypeId, Enemy> _enemies;
        private readonly LocationProvider _locationProvider;

        public EnemyFactory(DiContainer diContainer, LocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
            _enemies = Resources.LoadAll<Enemy>("Prefabs/Enemy").ToDictionary(x => x.EnemyTypeId, x=> x);
            _diContainer = diContainer;
        }

        public Enemy CreateBy(EnemyTypeId enemyTypeId)
        {
            Enemy enemy = _enemies[enemyTypeId];
            enemy.gameObject.SetActive(false);
            return _diContainer.InstantiatePrefabForComponent<Enemy>(enemy,_locationProvider.Values[LocationTypeId.EnemyParent]);
        }
    }
}