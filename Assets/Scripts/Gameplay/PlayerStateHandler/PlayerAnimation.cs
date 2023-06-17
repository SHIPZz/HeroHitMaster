﻿using UnityEngine;

namespace Gameplay.PlayerStateHandler
{
    public class PlayerAnimation : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private readonly float _maxSpeed = 5f;
        private Rigidbody _rigidbody;
        private Animator _animator;
        private bool _isRunning;
        private static readonly int IsRun = Animator.StringToHash("IsRun");

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var currentSpeed = _rigidbody.velocity.magnitude / _maxSpeed;
            _animator.SetFloat(Speed, currentSpeed);
            _animator.SetBool(IsRun, _isRunning);
        }

        public void SetRunning(bool isRunning)
        {
            _isRunning = isRunning;
        }
    }
}