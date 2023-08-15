using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Factories;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        [SerializeField] private TriggerObserver _aggroZone;

        private EnemyFactory _enemyFactory;

        public event Action Destroyed;
        public event Action<Enemy, TriggerObserver> Spawned;

        [Inject]
        private void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public void Init(Action<Enemy, TriggerObserver> callback)
        {
            Enemy enemy = _enemyFactory.CreateBy(_enemyTypeId);
            enemy.gameObject.transform.position = transform.position;
            enemy.Dead += Disable;
            callback?.Invoke(enemy, _aggroZone);
        }

        private void Disable(Enemy enemy)
        {
            Destroyed?.Invoke();
            gameObject.SetActive(false);
        }
    }
}