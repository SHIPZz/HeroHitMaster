using System;
using CodeBase.Gameplay.Character.Enemy;
using Zenject;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartMediator : IInitializable, IDisposable
    {
        private readonly EnemyBodyPartPositionSetter _enemyBodyPartPositionSetter;
        private readonly Enemy _enemy;
        private readonly DieOnAnimationEvent _dieOnAnimationEvent;

        public EnemyBodyPartMediator(EnemyBodyPartPositionSetter enemyBodyPartPositionSetter, Enemy enemy, DieOnAnimationEvent dieOnAnimationEvent)
        {
            _dieOnAnimationEvent = dieOnAnimationEvent;
            _enemyBodyPartPositionSetter = enemyBodyPartPositionSetter;
            _enemy = enemy;
        }

        public void Initialize()
        {
            _dieOnAnimationEvent.Dead += _enemyBodyPartPositionSetter.SetPosition;
            _enemy.QuickDestroyed += _enemyBodyPartPositionSetter.SetPosition;
        }

        public void Dispose()
        {
            _dieOnAnimationEvent.Dead -= _enemyBodyPartPositionSetter.SetPosition;
            _enemy.QuickDestroyed -= _enemyBodyPartPositionSetter.SetPosition;
        }
    }
}