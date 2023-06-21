using System;
using ScriptableObjects;
using Services;
using Services.Providers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerMediator : IDisposable, ITickable
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerAnimation _playerAnimation;
        private readonly UnityEngine.Camera _camera;
        private readonly IInputService _inputService;
        private readonly CameraProvider _cameraProvider;
        private readonly InputAction _moveAction;
        private readonly InputAction _jumpAction;
        private readonly InputAction _runAction;
        private readonly float _runningAnimationSpeed = 1f;
        private readonly float _walkingAnimationSpeed = 0.5f;
        private readonly float _jumpingTime = 0.5f;

        private bool _isRunning;
        private Vector2 _moveDirection;
        private float _jumpTimer;

        public PlayerMediator(PlayerMovement playerMovement, PlayerAnimation playerAnimation,
            IInputService inputService, CameraProvider cameraProvider)
        {
            _playerMovement = playerMovement;
            _playerAnimation = playerAnimation;
            _inputService = inputService;
            _cameraProvider = cameraProvider;

            _moveAction = _inputService.PlayerMove;
            _jumpAction = _inputService.PlayerJump;
            _runAction = _inputService.PlayerRun;

            _jumpAction.performed += OnJumpAction;
            _jumpAction.canceled += OnJumpAction;
            _runAction.performed += OnRunAction;
            _runAction.canceled += OnRunAction;
            _inputService.PlayerActions.Enable();
            _walkingAnimationSpeed = 0.5f;
        }

        public void Dispose()
        {
            _jumpAction.performed -= OnJumpAction;
            _jumpAction.canceled -= OnJumpAction;
            _runAction.performed -= OnRunAction;
            _runAction.canceled -= OnRunAction;
            _inputService.PlayerActions.Disable();
        }

        private void OnJumpAction(InputAction.CallbackContext context)
        {
            var isJumping = context.ReadValueAsButton();


            _playerMovement.SetJumping(isJumping);
            _playerAnimation.SetJumping(isJumping);
        }

        private void OnRunAction(InputAction.CallbackContext context)
        {
            _isRunning = context.ReadValueAsButton();
            _playerMovement.SetRunning(_isRunning);
            _playerAnimation.SetRunning(_isRunning);
        }

        public void Tick()
        {
            _moveDirection = _moveAction.ReadValue<Vector2>().normalized;

            _playerMovement.Move(_moveDirection, _cameraProvider.Camera);

            float speed = _moveDirection == Vector2.zero ? 0f :
                _isRunning ? _runningAnimationSpeed : _walkingAnimationSpeed;
            _playerAnimation.SetSpeed(speed, 0.1f, Time.deltaTime);
        }
    }
}