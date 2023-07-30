using System;
using CodeBase.Gameplay.Collision;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class AnimOnAgentMoving : IInitializable, ITickable, IDisposable
    {
        private const float MinimalVelocity = 0.1f;

        private readonly NavMeshAgent _navMeshAgent;
        private readonly EnemyAnimator _enemyAnimator;
        private TriggerObserver _triggerObserver;
        private bool _isBlocked;

        public AnimOnAgentMoving(NavMeshAgent navMeshAgent, EnemyAnimator enemyAnimator,
            TriggerObserver triggerObserver)
        {
            _triggerObserver = triggerObserver;
            _navMeshAgent = navMeshAgent;
            _enemyAnimator = enemyAnimator;
        }

        public void Initialize() =>
            _triggerObserver.Entered += DisableMovementAnimation;

        public void Tick()
        {
            if (!ShouldMove())
            {
                return;
            }

            _enemyAnimator.SetMovement(_navMeshAgent.velocity.magnitude);
            _isBlocked = false;
        }

        public void Dispose() =>
            _triggerObserver.Entered -= DisableMovementAnimation;

        private bool ShouldMove() =>
            _navMeshAgent.velocity.magnitude > MinimalVelocity && !_isBlocked;

        private void DisableMovementAnimation(Collider obj)
        {
            _enemyAnimator.StopMovement();
            _isBlocked = true;
        }
    }
}