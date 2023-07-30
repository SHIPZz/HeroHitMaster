using System;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Storages;
using Enums;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        [SerializeField] private TriggerObserver _aggroZone;

        private IEnemyStorage _enemyStorage;

        public event Action<Enemy,TriggerObserver> Spawned; 

        [Inject]
        private void Construct(IEnemyStorage enemyStorage) => 
            _enemyStorage = enemyStorage;

        private void Awake()
        {
            Enemy enemy = _enemyStorage.Get(_enemyTypeId);
            enemy.gameObject.transform.position = transform.position;
            Spawned?.Invoke(enemy, _aggroZone);
        }
    }
}