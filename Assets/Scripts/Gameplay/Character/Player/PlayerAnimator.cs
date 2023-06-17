using System;
using UnityEngine;

namespace Gameplay.Character.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int Walking = Animator.StringToHash("Walk");
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Walk(float speed)
        {
            
        }
    }
}