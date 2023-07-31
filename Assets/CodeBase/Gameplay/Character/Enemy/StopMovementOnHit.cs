using System;
using CodeBase.Constants;
using DG.Tweening;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class StopMovementOnHit : IInitializable, IDisposable
    {
        private readonly EnemyFollower _enemyFollower;
        private readonly IHealth _health;
        private bool _isDead;

        public StopMovementOnHit(EnemyFollower enemyFollower, IHealth health)
        {
            _enemyFollower = enemyFollower;
            _health = health;
        }

        public void Initialize()
        {
            _health.ValueChanged += Stop;
            _health.ValueZeroReached += Stop;
        }

        public void Dispose()
        {
            _health.ValueChanged -= Stop;
            _health.ValueZeroReached -= Stop;
        }

        private void Stop()
        {
            _isDead = true;
            _enemyFollower.Block();
        }

        private void Stop(int obj)
        {
            if (_isDead)
                return;
            
            _enemyFollower.Block();
            DOTween.Sequence().AppendInterval(DelayValues.MovementHitAnimationDelay).OnComplete(_enemyFollower.Unblock);
        }
    }
}