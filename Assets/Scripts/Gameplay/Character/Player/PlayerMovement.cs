using System;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay.Character.Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerMovement : MonoBehaviour
    {
        private IInputService _inputService;
        private InputAction _playerMove;
        private InputAction _playerJump;
        private PlayerAnimator _playerAnimator;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _playerMove = _inputService.PlayerMove;
            _playerJump = _inputService.PlayerJump;
        }

        private void Awake()
        {
            _playerAnimator = GetComponent<PlayerAnimator>();
            _playerMove.performed += OnMovePerformed;
            _playerMove.canceled += OnMoveCanceled;
        }

        private void OnDisable()
        {
            _playerMove.performed -= OnMovePerformed;
            _playerMove.canceled -= OnMoveCanceled;
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            // _playerAnimator.Walk(context.ReadValue<Vector2>().magnitude);
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            // var direction = _playerMove.ReadValue<Vector2>();
            // var direction3d = new Vector3(direction.x, 0, direction.y);
            // _playerAnimator.Walk(direction3d.magnitude);
        }
    }
}