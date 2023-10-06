using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Traps.Overhead
{
    public class OverheadTrap : Trap
    {
        [SerializeField] private float _disableDelay = 4f;
        [SerializeField] private Animator _animator;
        
        private static readonly int _isPressed = Animator.StringToHash("IsPressed");

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
                Activate();
        }

        public override void Activate()
        {
            _animator.SetBool(_isPressed, true);
            DOTween
                .Sequence()
            .AppendInterval(_disableDelay)
                .OnComplete(() => _animator.SetBool(_isPressed, false));
        }
    }
}