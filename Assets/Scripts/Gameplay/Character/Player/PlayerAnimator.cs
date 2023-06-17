using System;
using UnityEngine;

namespace Gameplay.Character.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int IsWalkedHash = Animator.StringToHash("IsWalked");
        private static readonly int IsRunningHash = Animator.StringToHash("IsRun");
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Walk(bool isWalked)
        {
            _animator.SetBool(IsWalkedHash, isWalked);
        }

        public void Run(bool isRun)
        {
            _animator.SetBool(IsRunningHash, isRun);
        }
    }
}