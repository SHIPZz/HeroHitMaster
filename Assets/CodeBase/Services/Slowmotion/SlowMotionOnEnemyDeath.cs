using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.MaterialChanger;
using Zenject;

namespace CodeBase.Services.Slowmotion
{
    public class SlowMotionOnEnemyDeath : IInitializable, IDisposable
    {
        private readonly GlobalSlowMotionSystem _globalSlowMotionSystem;
        private readonly List<EnemyQuantityInZone> _enemyZones;

        public SlowMotionOnEnemyDeath(GlobalSlowMotionSystem globalSlowMotionSystem, EnemyQuantityZonesProvider enemyQuantityZonesProvider)
        {
            _globalSlowMotionSystem = globalSlowMotionSystem;
            _enemyZones = enemyQuantityZonesProvider.EnemyQuantityInZones;
        }

        public void Initialize() => 
            _enemyZones.ForEach(x=>x.ZoneCleared += StartSlowMotion);

        public void Dispose() => 
            _enemyZones.ForEach(x=>x.ZoneCleared += StartSlowMotion);

        private void StartSlowMotion() => 
            _globalSlowMotionSystem.StartSlowMotion(0.3f, 1f, 0.1f);
    }
}