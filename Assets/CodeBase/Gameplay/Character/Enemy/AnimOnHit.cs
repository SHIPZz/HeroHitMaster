﻿using System;
using CodeBase.Constants;
using DG.Tweening;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class AnimOnHit : IInitializable, IDisposable
    {
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

            DOTween.Sequence().AppendInterval(DelayValues.MovementHitAnimationDelay).OnComplete(() =>
            {
                _enemyAnimator.SetIsDamaged(false);
                _enemyAnimator.SetMovement(1f);
            });
        }
    }
}