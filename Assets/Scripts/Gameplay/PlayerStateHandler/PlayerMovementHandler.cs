using Services;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay.PlayerStateHandler
{
    public class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField] private float _movementForce = 1f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _maxSpeed = 5f;

        private readonly float _valueForStartRunning = 0.5f;

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
        private float _additionalForce = 2f;

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
            _runningForce = _movementForce * _additionalForce;
            _initalMovementForce = _movementForce;
        }

        private void OnEnable()
        {
            _jumpAction.performed += OnJumpedAction;
            _inputService.PlayerActions.Enable();
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

        private void FixedUpdate()
        {
            if (_isRunning == false)
                SetMovementForce(_initalMovementForce);

            HandleMovement();
            ApplyGravity();
            UpdatePlayerAnimation();
            LookAt();
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
                SetMovementForce(_initalMovementForce);
                _playerAnimation.SetRunning(false);
                return;
            }

            _playerAnimation.SetRunning(_isRunning);
            SetMovementForce(_runningForce);
        }

        private void HandleMovement()
        {
            _forceDirection += _moveAction.ReadValue<Vector2>().x * Vector3.right * _movementForce;
            _forceDirection += _moveAction.ReadValue<Vector2>().y * Vector3.forward * _movementForce;

            _rigidbody.AddForce(_forceDirection, ForceMode.Impulse);
            _forceDirection = Vector3.zero;
        }

        private void ApplyGravity()
        {
            if (_rigidbody.velocity.y < 0f)
                _rigidbody.velocity += Vector3.down * (Physics.gravity.y * Time.fixedDeltaTime);
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

        private void SetMovementForce(float force) =>
            _movementForce = force;

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