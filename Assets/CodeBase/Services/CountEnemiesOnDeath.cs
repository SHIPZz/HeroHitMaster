using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;

namespace CodeBase.Services
{
    public class CountEnemiesOnDeath : IDisposable
    {
        private List<Enemy> _enemies =new();
        
        public int Quantity { get; private set; }

        private List<Enemy> _killedEnemies = new();

        public void Init(Enemy enemy)
        {
            enemy.QuickDestroyed += Count;
            enemy.GetComponent<DieOnAnimationEvent>().Dead += Count;
            _enemies.Add(enemy);
        }

        public void Dispose()
        {
            _enemies.ForEach(x =>
            {
                x.QuickDestroyed -= Count;
                x.GetComponent<DieOnAnimationEvent>().Dead -= Count;
            });
        }

        private void Count(Enemy enemy)
        {
            if(_killedEnemies.Contains(enemy))
                return;
            
            _killedEnemies.Add(enemy);
            Quantity++;
        }
    }
}