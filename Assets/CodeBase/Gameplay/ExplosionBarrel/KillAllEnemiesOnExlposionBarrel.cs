using System;
using System.Collections.Generic;
using System.Linq;
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
        private Collider _collider;

        public bool HasInactiveEnemies =>
            _enemies.Any(x => x.transform.localScale == Vector3.zero || !x.gameObject.activeSelf);

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
            _enemies.ForEach(x => x.Explode());
        }

        private void FillList(Enemy enemy) =>
            _enemies.Add(enemy);
    }
}