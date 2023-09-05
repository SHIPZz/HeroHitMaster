using UnityEngine;

namespace CodeBase.Gameplay.Character.Players
{
    public class PlayerAnimator
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsRun = Animator.StringToHash("IsRun");
        private static readonly int IsJumping = Animator.StringToHash("Jumping");
        private readonly Animator _animator;
        private static readonly int IsShooted = Animator.StringToHash("IsShooted");
        private static readonly int _isIdle = Animator.StringToHash("IsIdle");
        private static readonly int _isMoved = Animator.StringToHash("IsMoved");
        private bool _isIdleBlocked;

        public PlayerAnimator(Animator animator) =>
            _animator = animator;

        public void SetMovement(float speed)
        {
            _animator.SetBool(IsRun, true);
            _animator.SetFloat(Speed, speed);
        }

        public void NeedBlockIdle(bool enable) => 
            _isIdleBlocked = enable;
        
        public void StopMovement()
        {
            if (_isIdleBlocked)
                return;
            
            _animator.SetBool(IsRun, false);
            // _animator.SetFloat(Speed);
        }

        public void SetShooting(bool isShooting) =>
            _animator.SetBool(IsShooted, isShooting);
        
    }
}