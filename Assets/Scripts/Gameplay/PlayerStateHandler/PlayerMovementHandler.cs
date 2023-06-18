using System;
using Services;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay.PlayerStateHandler
{
    public class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _maxSpeed = 7f;
        [SerializeField] private LayerMask _layerMask;

        private readonly float _valueForStartRunning = 0.5f;
        private readonly float _additionalForce = 2f;

        private float _initalMovementForce;
        private Vector3 _forceDirection = Vector3.zero;
        private IInputService _inputService;
        private PlayerAnimation _playerAnimation;
        private Rigidbody _rigidbody;
        private float _runningForce;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _runAction;

        private UnityEngine.Camera _camera;
        private bool _isRunning;
        private CharacterController _characterController;

        [Inject]
        public void Construct(IInputService inputService, UnityEngine.Camera camera)
        {
            _inputService = inputService;
            _camera = camera;
            _moveAction = _inputService.PlayerMove;
            _jumpAction = _inputService.PlayerJump;
            _runAction = _inputService.PlayerRun;
        }

        private void Awake()
        {
            // _playerAnimator = GetComponent<PlayerAnimator>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rigidbody = GetComponent<Rigidbody>();
            _inputService.PlayerActions.Enable();
            _characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            _inputService.PlayerActions.Enable();
            _jumpAction.performed += OnJumpedAction;
            _runAction.performed += OnRunActionPerformed;
            _runAction.canceled += OnRunActionCanceled;
        }

        private void OnDisable()
        {
            _jumpAction.performed -= OnJumpedAction;
            _runAction.performed -= OnRunActionPerformed;
            _runAction.canceled -= OnRunActionCanceled;
            _inputService.PlayerActions.Disable();
        }

        private void Update()
        {
            // if (_isRunning == false)
            //     SetMovementForce(_initalMovementForce);

            // if (IsColliding())
            //     return;

            HandleMovement();
            UpdatePlayerAnimation();
            LookAt();
        }

        private bool IsColliding()
        {
            RaycastHit hit;
            float maxDistance = 3f;

            var start = transform.position + Vector3.up * 2f;
            Vector3 movementDirection = _rigidbody.velocity.normalized;
            var end = transform.position + movementDirection * maxDistance;

            Debug.DrawLine(start, end, Color.red, 0.1f);

            if (Physics.Linecast(start, end, out hit, _layerMask))
            {
                if (_layerMask.value == 8)
                {
                    print(hit.collider.name);
                    _forceDirection = Vector3.zero;
                    return true;
                }
            }

            return false;
        }

        private void OnRunActionPerformed(InputAction.CallbackContext context)
        {
            _isRunning = context.ReadValueAsButton();
        }

        private void OnRunActionCanceled(InputAction.CallbackContext context)
        {
            _isRunning = context.ReadValueAsButton();
        }

        private void UpdatePlayerAnimation()
        {
            var moveVector = _moveAction.ReadValue<Vector2>();

            if (!IsMoveVectorValid(moveVector))
            {
                _playerAnimation.SetRunning(false);
                return;
            }

            _playerAnimation.SetRunning(_isRunning);
        }

        private void HandleMovement()
        {
            var direction = _moveAction.ReadValue<Vector2>().normalized;
            _characterController.Move(new Vector3(direction.x,0, direction.y) * _maxSpeed * Time.deltaTime);
        }

        private void LookAt()
        {
            Vector3 direction = _rigidbody.velocity;
            direction.y = 0f;

            if (_moveAction.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
                _rigidbody.rotation = Quaternion.LookRotation(direction, Vector3.up);
            else
                _rigidbody.angularVelocity = Vector3.zero;
        }

        private void OnJumpedAction(InputAction.CallbackContext obj)
        {
            if (IsGrounded())
            {
                _forceDirection += Vector3.up * _jumpForce;
                print("111");
            }
        }

        private bool IsMoveVectorValid(Vector2 moveVector) =>
            moveVector.x > _valueForStartRunning || moveVector.y > _valueForStartRunning ||
            moveVector.x < -_valueForStartRunning || moveVector.y < -_valueForStartRunning;

        private bool IsGrounded()
        {
            Ray ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
                return true;
            else
                return false;
        }
    }
}