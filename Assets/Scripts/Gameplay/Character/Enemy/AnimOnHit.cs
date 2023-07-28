using System;
using Constants;
using DG.Tweening;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class AnimOnHit : IInitializable, IDisposable
    {
        private const float MovementAnimation = 1f;
        
        private readonly EnemyAnimator _enemyAnimator;
        private readonly IHealth _health;
        
        public AnimOnHit(EnemyAnimator enemyAnimator, IHealth health)
        {
            _enemyAnimator = enemyAnimator;
            _health = health;
        }

        public void Initialize()
        {
            _health.ValueChanged += PlayHitAnimation;
            _health.ValueZeroReached += PlayDieAnimation;
        }

        public void Dispose()
        {
            _health.ValueChanged -= PlayHitAnimation;
            _health.ValueZeroReached -= PlayDieAnimation;
        }

        private void PlayDieAnimation() => 
            _enemyAnimator.SetDeath();

        private void PlayHitAnimation(int obj)
        {
            _enemyAnimator.SetIsDamaged(true);
            _enemyAnimator.StopMovement();

            DOTween.Sequence().AppendInterval(DelayValues.MovementHitDelay).OnComplete(() =>
            {
                _enemyAnimator.SetIsDamaged(false);
                _enemyAnimator.SetMovement(MovementAnimation);
            });
        }
    }
}