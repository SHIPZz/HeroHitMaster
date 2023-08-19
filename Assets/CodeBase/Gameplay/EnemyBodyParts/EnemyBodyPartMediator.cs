using System;
using CodeBase.Gameplay.Character.Enemy;
using Zenject;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartMediator : IInitializable, IDisposable
    {
        private readonly EnemyBodyPartActivator _enemyBodyPartActivator;
        private readonly EnemyBodyPartPositionSetter _enemyBodyPartPositionSetter;
        private readonly Enemy _enemy;

        public EnemyBodyPartMediator(EnemyBodyPartActivator enemyBodyPartActivator, 
            EnemyBodyPartPositionSetter enemyBodyPartPositionSetter, Enemy enemy)
        {
            _enemy = enemy;
            _enemyBodyPartPositionSetter = enemyBodyPartPositionSetter;
            _enemyBodyPartActivator = enemyBodyPartActivator;
        }

        public void Initialize()
        {
            _enemy.Dead += _enemyBodyPartActivator.ActivateWithDisableDelay;
            _enemy.Dead += _enemyBodyPartPositionSetter.SetPosition;
        }

        public void Dispose()
        {
            // _enemy.Dead -= _enemyBodyPartActivator.ActivateWithDisableDelay;
            // _enemy.Dead -= _enemyBodyPartPositionSetter.SetPosition;
        }
    }
}