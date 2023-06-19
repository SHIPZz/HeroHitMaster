using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PlayerAnimation
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsRun = Animator.StringToHash("IsRun");
        private readonly Animator _animator;
        
        public PlayerAnimation(Animator animator) =>
            _animator = animator;

        public void SetSpeed(float speed) =>
            _animator.SetFloat(Speed, speed);

        public void SetRunning(bool isRunning) => 
            _animator.SetBool(IsRun, isRunning);

        public void SetJumping(bool isJumping)
        {
            
        }
    }
}