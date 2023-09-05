using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Gameplay.Character.Players
{
    public class AnimPlayerOnMove : ITickable
    {
        private readonly PlayerAnimator _playerAnimator;
        private readonly NavMeshAgent _navMeshAgent;

        public AnimPlayerOnMove(PlayerAnimator playerAnimator, NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
            _playerAnimator = playerAnimator;
        }

        public void Tick()
        {
            if (ShouldMove())
            {
                _playerAnimator.SetMovement(_navMeshAgent.velocity.magnitude);
                return;
            }

            _playerAnimator.StopMovement();
        }

        private bool ShouldMove() =>
            _navMeshAgent.velocity.magnitude > 0.1;
    }
}