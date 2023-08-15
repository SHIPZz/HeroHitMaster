using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.Providers.AssetProviders;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class EnemyFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IProvider<LocationTypeId, Transform> _locationProvider;
        private readonly EnemyStaticDataService _enemyStaticDataService;
        private AssetProvider _assetProvider;

        public EnemyFactory(DiContainer diContainer, IProvider<LocationTypeId, Transform> locationProvider, 
            EnemyStaticDataService enemyStaticDataService, AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _enemyStaticDataService = enemyStaticDataService;
            _locationProvider = locationProvider;
            
            _diContainer = diContainer;
        }

        public Enemy CreateBy(EnemyTypeId enemyTypeId)
        {
            var enemy = _enemyStaticDataService.GetEnemyData(enemyTypeId)
                .Prefab.GetComponent<Enemy>();
            return _diContainer
                .InstantiatePrefabForComponent<Enemy>(enemy, 
                _locationProvider.Get(LocationTypeId.EnemyParent));
        }

        // public async UniTask<Dictionary<EnemyTypeId, Enemy>> CreateAll()
        // {
        //     var enemies = new Dictionary<EnemyTypeId, Enemy>();
        //     
        //     foreach (var enemyData in _enemyStaticDataService.GetAll())
        //     {
        //         Enemy enemy = await CreateBy(enemyData.EnemyTypeId);
        //         enemies[enemy.EnemyTypeId] = enemy;
        //     }
        //
        //     return enemies;
        // }

        // public async UniTask<Enemy> CreateBy(EnemyTypeId enemyTypeId)
        // {
        //     var prefab = await _assetProvider.Load<GameObject>(_enemyStaticDataService.GetEnemyData(enemyTypeId).PrefabReferencee);
        //
        //     Enemy enemy = prefab.GetComponent<Enemy>();
        //     enemy.gameObject.SetActive(false);
        //     return _diContainer
        //         .InstantiatePrefabForComponent<Enemy>(enemy,
        //         _locationProvider.Get(LocationTypeId.EnemyParent));
        // }
    }
}