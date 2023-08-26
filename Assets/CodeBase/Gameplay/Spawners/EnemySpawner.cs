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
            enemy.GetComponent<IMaterialChanger>().StartedChanged += DisableWithDelay;
            enemy.QuickDestroyed += Disable;
            callback?.Invoke(enemy, _aggroZone);
        }

        private void DisableWithDelay()
        {
            DOTween.Sequence().AppendInterval(1f).OnComplete(() =>
            {
                Destroyed?.Invoke();
                gameObject.SetActive(false);
            });
        }

        private void Disable(Enemy enemy)
        {
            Destroyed?.Invoke();
            gameObject.SetActive(false);
        }
    }
}