using Gameplay.Character.Player;
using UnityEngine;

namespace Gameplay.PlayerStateHandler.States
{
    public class IdleState : State
    {
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();
            StateId = StateId.Idle;
        }

        public override void Enable()
        {
            Animator.SetBool(IsIdle, true);
        }

        public override void Exit()
        {
            Animator.SetBool(IsIdle, false);
        }
    }
}