using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerMovement
    {
        private const float Gravity = -9.8f;

        private float _speed;
        private float _speedY;

        private bool _isRunning;
        private CharacterController _characterController;
        private Player _player;
        private PlayerMovementSettings _playerMovementSettings;
        private bool _isJumping;
        private float _angleOfMovement;
        private Rigidbody _rigidBody;

        [Inject]
        public void Construct(CharacterController characterController, Player player,
            PlayerMovementSettings playerMovementSettings, Rigidbody rigidbody)
        {
            _rigidBody = rigidbody;
            _playerMovementSettings = playerMovementSettings;
            _player = player;
            _characterController = characterController;
        }

        public void Move(Vector3 moveDirection, UnityEngine.Camera camera)
        {
            Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y);

            // _speedY -= Gravity * Time.deltaTime;
            // _speedY += _characterController.isGrounded ? 0 : Gravity * Time.deltaTime;

            Vector3 rotatedMovement = MoveToDirection(camera, movement);

            if (moveDirection.magnitude > 0)
            {
                _angleOfMovement = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            }

            if (moveDirection.magnitude > 0)
            {
                SetSpeed();
            }

            Rotate();
        }

        private Vector3 MoveToDirection(UnityEngine.Camera camera, Vector3 movement)
        {
            Vector3 rotatedMovement = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0) * movement;
            // Vector3 verticalMovement = Vector3.up * _speedY;
            // _characterController.Move((verticalMovement + (rotatedMovement * _speed) * Time.deltaTime));
            _characterController.Move(rotatedMovement * _speed * Time.deltaTime);
            return rotatedMovement;
        }

        private void Rotate() =>
            _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation,
                Quaternion.Euler(0, _angleOfMovement, 0f),
                _playerMovementSettings.RotationSpeed * Time.deltaTime);

        public void Jump()
        {
            /*_player.transform.position += new Vector3(0, 3, 0)*/;
            _rigidBody.AddForce(_playerMovementSettings.JumpForce * Vector3.up);
            if (_characterController.isGrounded)
            {
                // _speedY = 0;
                // _characterController.Move(_player.transform.position + new Vector3(0, 3, 0));
            }
        }


        public void SetRunning(bool isRunning) =>
            _isRunning = isRunning;

        public void SetJumping(bool isJumping) =>
            _isJumping = isJumping;

        private void SetSpeed() =>
            _speed = _isRunning ? _playerMovementSettings.RunSpeed : _playerMovementSettings.WalkingSpeed;
    }
}