using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Spawners;
using UnityEngine;

namespace CodeBase.Gameplay.ExplosionBarrel
{
    public class KillAllEnemiesOnExlposionBarrel : MonoBehaviour
    {
        [SerializeField] private ExplosionBarrel _explosionBarrel;
        [SerializeField] private List<EnemySpawner> _enemySpawners;

        private List<Enemy> _enemies = new();

        private void OnEnable()
        {
            _enemySpawners.ForEach(x => x.Spawned += FillList);
            _explosionBarrel.Exploded += Kill;
        }

        private void OnDisable()
        {
            _explosionBarrel.Exploded -= Kill;
            _enemySpawners.ForEach(x => x.Spawned -= FillList);
        }

        private void Kill()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Explode();
            }
        }

        private void FillList(Enemy enemy) =>
            _enemies.Add(enemy);
    }
}