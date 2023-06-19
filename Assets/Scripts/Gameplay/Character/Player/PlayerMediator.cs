using System;
using ScriptableObjects;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerMediator : IDisposable, ITickable
    {        
        private const float Gravity = -9.8f;
        
        private readonly PlayerMovementHandler _playerMovementHandler;
        private readonly PlayerAnimation _playerAnimation;
        private readonly IInputService _inputService;
        private readonly PlayerMovementSettings _playerMovementSettings;
        private readonly InputAction _moveAction;
        private readonly InputAction _jumpAction;
        private readonly InputAction _runAction;

        private Vector3 _direction;
        private Vector2 _moveDirection;
        private float _speed;
        private Vector3 _velocity;

        public PlayerMediator(PlayerMovementHandler playerMovementHandler, PlayerAnimation playerAnimation,
            IInputService inputService, PlayerMovementSettings playerMovementSettings)
        {
            _playerMovementHandler = playerMovementHandler;
            _playerAnimation = playerAnimation;
            _inputService = inputService;
            _playerMovementSettings = playerMovementSettings;

            _moveAction = _inputService.PlayerMove;
            _jumpAction = _inputService.PlayerJump;
            _runAction = _inputService.PlayerRun;

            _jumpAction.performed += OnJumpedAction;
            _runAction.performed += OnRunActionPerformed;
            _runAction.canceled += OnRunActionCanceled;
            _inputService.PlayerActions.Enable();
            _speed = _playerMovementSettings.WalkingSpeed;
        }
        
        public void Dispose()
        {
            _jumpAction.performed -= OnJumpedAction;
            _runAction.performed -= OnRunActionPerformed;
            _runAction.canceled -= OnRunActionCanceled;
            _inputService.PlayerActions.Disable();
        }

        private void OnJumpedAction(InputAction.CallbackContext obj)
        {
            _velocity.y = _playerMovementSettings.JumpForce;
            _playerMovementHandler.Jump(_playerMovementSettings.JumpForce, _playerMovementSettings.JumpSpeed);
            // _playerMovementHandler.Jump();
            _playerAnimation.SetJumping(true);
        }

        private void OnRunActionPerformed(InputAction.CallbackContext context)
        {
            _speed = _playerMovementSettings.RunSpeed;
            // _playerMovementHandler.Run();
            // _direction = context.ReadValue<Vector2>();
            _playerAnimation.SetRunning(true);
        }

        private void OnRunActionCanceled(InputAction.CallbackContext context)
        {
            _speed = _playerMovementSettings.WalkingSpeed;
            // _playerMovementHandler.StopRun();
            _playerAnimation.SetRunning(false);
        }

        public void Tick()
        {
            _moveDirection = _moveAction.ReadValue<Vector2>();
            _playerMovementHandler.Move(_moveDirection, _speed);
            _playerAnimation.SetSpeed(_playerMovementHandler.CharacterController.velocity.magnitude / _speed);
            _playerMovementHandler.LookAt(_moveDirection, _playerMovementSettings.RotationSpeed);
            _velocity.y += Gravity * Time.deltaTime;
            _playerMovementHandler.CharacterController.Move(_velocity * _playerMovementSettings.JumpSpeed * Time.deltaTime);
        }
    }
}