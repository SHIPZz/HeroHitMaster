using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyAnimator
    {
        private static readonly int _isAttacked = Animator.StringToHash("IsAttacked");
        private static readonly int _isMoved = Animator.StringToHash("IsMoved");
        private static readonly int _isDamaged = Animator.StringToHash("IsDamaged");
        private static readonly int _speed = Animator.StringToHash("Speed");
        private readonly Animator _animator;
        private static readonly int _die = Animator.StringToHash("Die");
        private static readonly int _win = Animator.StringToHash("Win");

        public EnemyAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void SetAttack(bool isAttacked) =>
            _animator.SetBool(_isAttacked, isAttacked);

        public void SetMovement(float speed)
        {
            _animator.SetBool(_isMoved, true);
            _animator.SetFloat(_speed, speed);
        }

        public void StopMovement() =>
            _animator.SetBool(_isMoved, false);

        public void SetIsDamaged(bool isDamaged) =>
            _animator.SetBool(_isDamaged, isDamaged);

        public void SetVictory() =>
            _animator.SetTrigger(_win);

        public void SetDeath() =>
            _animator.SetTrigger(_die);
    }
}