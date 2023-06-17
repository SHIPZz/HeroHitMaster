using Gameplay.Character.Player;
using UnityEngine;

namespace Gameplay.PlayerStateHandler.States
{
    public class WalkState : State
    {
        private static readonly int IsWalked = Animator.StringToHash("IsWalked");
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();
            StateId = StateId.Walk;
        }

        public override void Enable()
        {
            Animator.SetBool(IsWalked, true);
        }

        public override void Exit()
        {
            Animator.SetBool(IsWalked, false);
        }
    }
}