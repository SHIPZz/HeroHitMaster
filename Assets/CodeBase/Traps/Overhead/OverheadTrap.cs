using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Traps.Overhead
{
    public class OverheadTrap : Trap
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int _isPressed = Animator.StringToHash("IsPressed");

        public override void Activate()
        {
            _animator.SetBool(_isPressed, true);
            DOTween
                .Sequence()
            .AppendInterval(DisableDelay)
                .OnComplete(() => _animator.SetBool(_isPressed, false));
        }
    }
}