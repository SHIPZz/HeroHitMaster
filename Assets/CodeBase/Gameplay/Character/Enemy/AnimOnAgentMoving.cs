using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class AnimOnAgentMoving : ITickable
    {
        private const float MinimalVelocity = 0.1f;

        private readonly NavMeshAgent _navMeshAgent;
        private readonly EnemyAnimator _enemyAnimator;

        public AnimOnAgentMoving(NavMeshAgent navMeshAgent, EnemyAnimator enemyAnimator)
        {
            _navMeshAgent = navMeshAgent;
            _enemyAnimator = enemyAnimator;
        }

        public void Tick()
        {
            if (!ShouldMove())
            {
                _enemyAnimator.StopMovement();
                return;
            }

            _enemyAnimator.SetMovement(_navMeshAgent.velocity.magnitude);
        }

        private bool ShouldMove() =>
            _navMeshAgent.velocity.magnitude > MinimalVelocity;
    }
}