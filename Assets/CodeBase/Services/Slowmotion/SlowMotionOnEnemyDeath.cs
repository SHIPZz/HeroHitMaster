using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.MaterialChanger;

namespace CodeBase.Services.Slowmotion
{
    public class SlowMotionOnEnemyDeath : IDisposable
    {
        private readonly GlobalSlowMotionSystem _globalSlowMotionSystem;
        private readonly List<Enemy> _enemies = new();
        private readonly List<Enemy> _deadEnemies = new();
        private bool _isPlayed;

        public SlowMotionOnEnemyDeath(GlobalSlowMotionSystem globalSlowMotionSystem)
        {
            _globalSlowMotionSystem = globalSlowMotionSystem;
        }

        public void Init(Enemy enemy)
        {
            enemy.Dead += StartSlowMotionOnDefaultDeath;
            enemy.QuickDestroyed += StartSlowMotionOnQuickDeath;
            enemy.GetComponent<IMaterialChanger>().StartedChanged += StartSlowMotionOnMaterialChangedDeath;
            _enemies.Add(enemy);
        }

        public void Dispose()
        {
            _enemies.ForEach(enemy =>
            {
                enemy.Dead -= StartSlowMotionOnDefaultDeath;
                enemy.QuickDestroyed -= StartSlowMotionOnQuickDeath;
                // enemy.GetComponent<IMaterialChanger>().StartedChanged -= StartSlowMotionOnMaterialChangedDeath;
            });
        }

        private void StartSlowMotionOnDefaultDeath(Enemy obj)
        {
            if (_deadEnemies.Contains(obj) || obj.GetComponent<IMaterialChanger>().IsChanging)
                return;
            
            _globalSlowMotionSystem.StartSlowMotion(0.3f, 1f, 0.1f);
            _deadEnemies.Add(obj);
        }
        
        private void StartSlowMotionOnQuickDeath(Enemy obj)
        {
            if (_deadEnemies.Contains(obj))
                return;
            
            _globalSlowMotionSystem.StartSlowMotion(0.3f, 1f, 0.1f);
            _deadEnemies.Add(obj);
        }

        private void StartSlowMotionOnMaterialChangedDeath()
        {
            _globalSlowMotionSystem.StartSlowMotion(0.3f, 1f, 0.1f);
        }
    }
}