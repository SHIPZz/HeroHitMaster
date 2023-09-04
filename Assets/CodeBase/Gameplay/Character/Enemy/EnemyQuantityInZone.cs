using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Spawners;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyQuantityInZone : MonoBehaviour
    {
        [SerializeField] private List<EnemySpawner> _enemySpawners;

        private int _initialCount;
        private int _count;

        public event Action ZoneCleared;

        private void Awake() => 
        _initialCount = _enemySpawners.Count;

        private void OnEnable() =>
            _enemySpawners.ForEach(x => x.Destroyed += CountDeadEnemies);
        
        private void OnDisable() =>
            _enemySpawners.ForEach(x => x.Destroyed -= CountDeadEnemies);

        private void CountDeadEnemies()
        {
            _count++;

            if (_count != _initialCount) 
                return;
            
            ZoneCleared?.Invoke();
            gameObject.SetActive(false);
        }
    }
}