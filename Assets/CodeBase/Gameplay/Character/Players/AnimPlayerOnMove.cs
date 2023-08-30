using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Gameplay.Character.Players
{
    public class AnimPlayerOnMove : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
         private PlayerAnimator _playerAnimator;

         [Inject]
         private void Construct(PlayerAnimator playerAnimator) => 
             _playerAnimator = playerAnimator;

         private void Update()
        {
            if (ShouldMove())
            {
                _playerAnimator.SetMovement(_navMeshAgent.velocity.magnitude);
                return;
            }
            
            // _playerAnimator.StopMovement();
        }

        private bool ShouldMove() =>
            _navMeshAgent.velocity.magnitude > 0.1;
    }
}