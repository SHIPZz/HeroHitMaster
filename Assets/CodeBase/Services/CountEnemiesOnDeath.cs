using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.MaterialChanger;

namespace CodeBase.Services
{
    public class CountEnemiesOnDeath : IDisposable
    {
        private List<Enemy> _enemies = new();

        public int Quantity { get; private set; }

        private List<Enemy> _killedEnemies = new();

        public void Init(List<Enemy> enemies)
        {
            _enemies = enemies;
            
            foreach (Enemy enemy in _enemies)
            {
                enemy.QuickDestroyed -= Count;
                enemy.GetComponent<DieOnAnimationEvent>().Dead += Count;
                enemy.Dead -= Count;
            }
        }

        public void Dispose()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.QuickDestroyed -= Count;
                enemy.GetComponent<DieOnAnimationEvent>().Dead -= Count;
                enemy.Dead -= Count;
            }
        }

        private void Count(Enemy enemy)
        {
            if (_killedEnemies.Contains(enemy))
                return;

            _killedEnemies.Add(enemy);
            Quantity++;
        }
    }
}