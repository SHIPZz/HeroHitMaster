using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Character;
using Zenject;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartMediator : IInitializable, IDisposable
    {
        private readonly EnemyBodyPartActivator _enemyBodyPartActivator;
        private List<Enemy> _enemies;
        private readonly EnemyBodyPartPositionSetter _enemyBodyPartPositionSetter;
        private readonly IEnemyStorage _enemyStorage;

        public EnemyBodyPartMediator(IEnemyStorage enemyStorage, EnemyBodyPartActivator enemyBodyPartActivator, 
            EnemyBodyPartPositionSetter enemyBodyPartPositionSetter)
        {
            _enemyBodyPartPositionSetter = enemyBodyPartPositionSetter;
            _enemyBodyPartActivator = enemyBodyPartActivator;
            _enemyStorage = enemyStorage;
        }

        public  void Initialize()
        {
            // _enemies =  _enemyStorage.GetAll();
            //
            // foreach (var enemy in _enemies)
            // {
            //     enemy.Dead += _enemyBodyPartActivator.ActivateWithDisableDelay;
            //     enemy.Dead += _enemyBodyPartPositionSetter.SetPosition;
            // }
        }

        public void Dispose()
        {
            // foreach (var enemy in _enemies)
            // {
            //     enemy.Dead -= _enemyBodyPartActivator.ActivateWithDisableDelay;
            //     enemy.Dead -= _enemyBodyPartPositionSetter.SetPosition;
            // }
        }
    }
}