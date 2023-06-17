using Gameplay.Character.Player;
using UnityEngine;

namespace Gameplay.PlayerStateHandler.States
{
    public class RunState : State
    {
        private static readonly int IsRun = Animator.StringToHash("IsRun");

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            StateId = StateId.Run;
        }

        public override void Enable()
        {
            Animator.SetBool(IsRun, true);
        }

        public override void Exit()
        {
            Animator.SetBool(IsRun, false);
        }
    }
}