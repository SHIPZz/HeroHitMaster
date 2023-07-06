using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PlayerAnimator
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsRun = Animator.StringToHash("IsRun");
        private static readonly int IsJumping = Animator.StringToHash("Jumping");
        private readonly Animator _animator;
        private static readonly int IsShooted = Animator.StringToHash("IsShooted");

        public PlayerAnimator(Animator animator) =>
            _animator = animator;

        public void SetSpeed(float speed) =>
            _animator.SetFloat(Speed, speed);
        
        public void SetSpeed(float speed, float dampTime, float deltaTime) =>
            _animator.SetFloat(Speed, speed, dampTime, deltaTime);

        public void SetRunning(bool isRunning) => 
            _animator.SetBool(IsRun, isRunning);

        public void SetShooting(bool isShooting) =>
            _animator.SetBool(IsShooted, isShooting);

        public void SetJumping(bool isJumping)
        {
            _animator.SetTrigger(IsJumping);
        }

        public void SetLanding()
        {
            _animator.SetTrigger("IsLanding");
        }
    }
}