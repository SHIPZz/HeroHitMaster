using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Storages.Character;
using UnityEngine;

namespace CodeBase.Gameplay.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        [SerializeField] private TriggerObserver _aggroZone;

        public event Action Destroyed;
        public event Action<Enemy, TriggerObserver> Spawned;

        public void Init(IEnemyStorage enemyStorage)
        {
            Enemy enemy =  enemyStorage.Get(_enemyTypeId);
            enemy.gameObject.transform.position = transform.position;
            enemy.Dead += Disable;
            Spawned?.Invoke(enemy,_aggroZone);
        }

        private void Disable(Enemy enemy)
        {
            Destroyed?.Invoke();
            gameObject.SetActive(false);
        }
    }
}