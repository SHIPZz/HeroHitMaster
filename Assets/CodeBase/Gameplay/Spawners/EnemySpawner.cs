using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Services.Factories;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        [SerializeField] private TriggerObserver _aggroZone;

        private EnemyFactory _enemyFactory;
        private float _destroyDelay = 0;
        private bool _canDestroy = true;

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
            var materialChanger = enemy.GetComponent<IMaterialChanger>();
            Subscribe(enemy, materialChanger);
            callback?.Invoke(enemy, _aggroZone);
        }

        private void Subscribe(Enemy enemy, IMaterialChanger materialChanger)
        {
            enemy.Dead += Disable;
            materialChanger.StartedChanged += BlockDestruction;
            materialChanger.Completed += Disable;
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