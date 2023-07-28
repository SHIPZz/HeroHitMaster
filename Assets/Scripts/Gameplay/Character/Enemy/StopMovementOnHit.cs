using System;
using Constants;
using DG.Tweening;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class StopMovementOnHit : IInitializable, IDisposable
    {
        private readonly EnemyFollower _enemyFollower;
        private readonly IHealth _health;

        public StopMovementOnHit(EnemyFollower enemyFollower, IHealth health)
        {
            _enemyFollower = enemyFollower;
            _health = health;
        }

        public void Initialize()
        {
            _health.ValueChanged += Stop;
            _health.ValueZeroReached += _enemyFollower.Block;
        }

        public void Dispose()
        {
            _health.ValueChanged -= Stop;
            _health.ValueZeroReached -= _enemyFollower.Block;
        }

        private void Stop(int obj)
        {
            _enemyFollower.Block();
            DOTween.Sequence().AppendInterval(DelayValues.MovementHitDelay).OnComplete(_enemyFollower.Unblock);
        }
    }
}