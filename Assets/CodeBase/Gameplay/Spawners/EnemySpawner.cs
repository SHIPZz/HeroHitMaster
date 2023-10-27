using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Services.Factories;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        [SerializeField] private AggroZone _aggroZone;

        private EnemyFactory _enemyFactory;
        private bool _canDestroy = true;
        private Enemy _enemy;

        public event Action Destroyed;
        public event Action<Enemy> Spawned;

        [Inject]
        private void Construct(EnemyFactory enemyFactory) => 
            _enemyFactory = enemyFactory;

        public void Init(Action<Enemy, AggroZone> callback)
        {
            Enemy enemy = _enemyFactory.CreateBy(_enemyTypeId);
            _enemy = enemy;
            enemy.gameObject.transform.position = transform.position;
            var materialChanger = enemy.GetComponent<IMaterialChanger>();
            Subscribe(enemy, materialChanger);
            Spawned?.Invoke(enemy);
            callback?.Invoke(enemy, _aggroZone);
        }

        private void Subscribe(Enemy enemy, IMaterialChanger materialChanger)
        {
            enemy.Dead += Disable;
            materialChanger.StartedChanged += BlockDestruction;
            materialChanger.StartedChanged += Disable;
            enemy.QuickDestroyed += Disable;
        }

        private void Disable()
        {
            Destroyed?.Invoke();
            gameObject.SetActive(false);
        }

        private void BlockDestruction() =>
            _canDestroy = false;

        private void Disable(Enemy enemy)
        {
            if (!_canDestroy)
                return;
            
            Destroyed?.Invoke();
            gameObject.SetActive(false);
        }
    }
}