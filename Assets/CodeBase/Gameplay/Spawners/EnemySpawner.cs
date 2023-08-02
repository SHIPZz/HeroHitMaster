﻿using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Character;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        [SerializeField] private TriggerObserver _aggroZone;

        private IEnemyStorage _enemyStorage;

        [Inject]
        private void Construct(IEnemyStorage enemyStorage) => 
            _enemyStorage = enemyStorage;

        public void Init(Action<Enemy, TriggerObserver> callback)
        {
            Enemy enemy = _enemyStorage.Get(_enemyTypeId);
            enemy.gameObject.transform.position = transform.position;
            callback?.Invoke(enemy, _aggroZone);
        }
    }
}