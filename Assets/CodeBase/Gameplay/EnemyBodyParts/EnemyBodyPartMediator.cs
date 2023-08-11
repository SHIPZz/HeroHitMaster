using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Character;
using Zenject;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartMediator : IDisposable
    {
        private readonly EnemyBodyPartActivator _enemyBodyPartActivator;
        private readonly EnemyBodyPartPositionSetter _enemyBodyPartPositionSetter;
        private List<Enemy> _enemies;

        public EnemyBodyPartMediator(EnemyBodyPartActivator enemyBodyPartActivator, 
            EnemyBodyPartPositionSetter enemyBodyPartPositionSetter)
        {
            _enemyBodyPartPositionSetter = enemyBodyPartPositionSetter;
            _enemyBodyPartActivator = enemyBodyPartActivator;
        }

        public void Init(List<Enemy> enemies)
        {
            _enemies = enemies;
            
            foreach (var enemy in enemies)
            {
                enemy.Dead += _enemyBodyPartActivator.ActivateWithDisableDelay;
                enemy.Dead += _enemyBodyPartPositionSetter.SetPosition;
            }
        }

        public void Dispose()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Dead -= _enemyBodyPartActivator.ActivateWithDisableDelay;
                enemy.Dead -= _enemyBodyPartPositionSetter.SetPosition;
            }
        }
    }
}