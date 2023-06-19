using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerMovementHandler
    {
        private const float ValueForStartRunning = 0.5f;
        private const float AdditionalSpeed = 2f;
        private const float Gravity = -9.8f;

        private Vector3 _velocity;
        private float _initalSpeed;
        private Rigidbody _rigidbody;
        private float _runningForce;

        private bool _isRunning;
        private CharacterController _characterController;

        public CharacterController CharacterController =>
            _characterController;

        [Inject]
        public void Construct(Rigidbody rigidbody, CharacterController characterController)
        {
            _characterController = characterController;
            _rigidbody = rigidbody;
        }

        public void Move(Vector3 direction, float speed)
        {
            _characterController.Move(new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime);
        }

        public void Jump(float jumpForce, float speed)
        {
            _characterController.Move(new Vector3(0, jumpForce, 0) * speed * Time.deltaTime);
            if (_characterController.isGrounded) { }
        }

        private void Update()
        {
            // HandleMovement();
            // _velocity.y += Gravity * Time.deltaTime;
            // _characterController.Move(_velocity * _jumpSpeed * Time.deltaTime);
        }

        public void LookAt(Vector2 moveDirection, float rotationSpeed)
        {
            Vector3 direction = _characterController.velocity;
            direction.y = 0f;

            if (moveDirection.sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                _rigidbody.rotation =
                    Quaternion.Lerp(_rigidbody.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
            else
            {
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}