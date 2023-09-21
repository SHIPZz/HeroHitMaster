using System;
using CodeBase.Gameplay.Character.Enemy;
using Zenject;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartMediator : IInitializable, IDisposable
    {
        private readonly EnemyBodyPartPositionSetter _enemyBodyPartPositionSetter;
        private readonly Enemy _enemy;

        public EnemyBodyPartMediator(EnemyBodyPartPositionSetter enemyBodyPartPositionSetter, Enemy enemy)
        {
            _enemyBodyPartPositionSetter = enemyBodyPartPositionSetter;
            _enemy = enemy;
        }

        public void Initialize()
        {
            _enemy.Dead += _enemyBodyPartPositionSetter.SetPosition;
            _enemy.QuickDestroyed += _enemyBodyPartPositionSetter.SetPosition;
        }

        public void Dispose()
        {
            _enemy.Dead -= _enemyBodyPartPositionSetter.SetPosition;
            _enemy.QuickDestroyed -= _enemyBodyPartPositionSetter.SetPosition;
        }
    }
}