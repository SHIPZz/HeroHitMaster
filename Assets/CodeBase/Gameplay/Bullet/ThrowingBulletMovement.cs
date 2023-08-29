using CodeBase.Gameplay.Collision;
using DG.Tweening;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace CodeBase.Gameplay.Bullet
{
    public class ThrowingBulletMovement : BulletMovement
    {
        [SerializeField] private Vector3 _bulletModelRotation;
        [SerializeField] private Transform _knifeEnd;

        private TriggerObserver _triggerObserver;

        private ThrowingBullet _throwingBullet;
        private bool _isHit;
        private Vector3 _moveDirection;
        private bool _blockedRotation;
        private Coroutine _moveCoroutine;
        private Vector3 _hitPosition;

        protected override void Awake()
        {
            base.Awake();
            _throwingBullet = GetComponent<ThrowingBullet>();
            _triggerObserver = GetComponent<TriggerObserver>();
            SetInitialRotation(_throwingBullet);
            RigidBody.isKinematic = false;
            RigidBody.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private void OnEnable() =>
            _triggerObserver.CollisionEntered += OnCollisionEntered;

        private void OnDisable()
        {
            _triggerObserver.CollisionEntered -= OnCollisionEntered;
            _isHit = false;
        }

        private void FixedUpdate()
        {
            if (_moveDirection == Vector3.zero)
                return;

            if (_isHit)
            {
                RigidBody.velocity = Vector3.zero;
                RigidBody.angularVelocity = Vector3.zero;
                return;
            }

            RigidBody.velocity = _moveDirection * 35;
            RigidBody.angularVelocity = transform.right * 50;
        }

        public override void Move(Vector3 target, Vector3 startPosition)
        {
            _moveDirection = target - startPosition;
            _moveDirection.Normalize();
            transform.forward = _moveDirection;
            SetInitialRotation(_throwingBullet);
        }

        private void OnCollisionEntered(UnityEngine.Collision target)
        {
            _hitPosition = target.GetContact(0).point;
            transform.forward = _moveDirection;
            RigidBody.useGravity = false;
            RigidBody.velocity = Vector3.zero;
            RigidBody.angularVelocity = Vector3.zero;
            SetInitialRotation(_throwingBullet);

            if (target.gameObject.TryGetComponent(out EnemyPartForKnifeHolder enemyPartForKnifeHolder))
            {
                RigidBody.interpolation = RigidbodyInterpolation.None;
                transform.SetParent(target.transform);
            }

            Vector3 offset = _hitPosition - _knifeEnd.position;
            // transform.DOMove(transform.position + offset, 0.1f);
            RigidBody.position += offset; 

            _isHit = true;
        }
        
        private void SetInitialRotation(ThrowingBullet throwingBullet)
        {
            Vector3 startTargetRotation = new Vector3(104, transform.eulerAngles.y,
                transform.eulerAngles.z);

            transform.rotation = Quaternion.Euler(startTargetRotation);

            throwingBullet.BulletModel.localRotation = Quaternion.Euler(_bulletModelRotation);
        }
    }
}