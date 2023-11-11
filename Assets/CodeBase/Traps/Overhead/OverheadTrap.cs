using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Traps.Overhead
{
    public class OverheadTrap : Trap
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _disableColliderDelay = 1.2f;
        
        private static readonly int _isPressed = Animator.StringToHash("IsPressed");

        public override void Activate()
        {
            _animator.SetBool(_isPressed, true);

            DOTween
                .Sequence()
                .AppendInterval(_disableColliderDelay)
                .OnComplete(() => Collider.enabled = false);
            
            DOTween
                .Sequence()
            .AppendInterval(DisableDelay)
                .OnComplete(() =>
                {
                    Collider.enabled = true;
                    _animator.SetBool(_isPressed, false);
                });
        }
    }
}