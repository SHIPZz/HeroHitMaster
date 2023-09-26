using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;

namespace CodeBase.Services
{
    public class CountEnemiesOnDeath : IDisposable
    {
        private List<Enemy> _enemies =new();
        
        public int Quantity { get; private set; }

        public void Init(Enemy enemy)
        {
            enemy.Dead += Count;
            enemy.QuickDestroyed += Count;
            enemy.GetComponent<DieOnAnimationEvent>().Dead += Count;
            _enemies.Add(enemy);
        }

        public void Dispose()
        {
            _enemies.ForEach(x =>
            {
                x.Dead -= Count;
                x.QuickDestroyed -= Count;
                x.GetComponent<DieOnAnimationEvent>().Dead -= Count;
            });
        }

        private void Count(Enemy obj) => 
            Quantity++;
    }
}