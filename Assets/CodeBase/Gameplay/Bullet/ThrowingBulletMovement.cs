using System.Collections;
using CodeBase.Gameplay.Collision;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public class ThrowingBulletMovement : BulletMovement
    {
        [SerializeField] private Vector3 _bulletModelRotation;

        private TriggerObserver _triggerObserver;

        private ThrowingBullet _throwingBullet;
        private bool _isBlocked;
        private Vector3 _angularVelocity;
        private Vector3 _moveDirection;

        protected override void Awake()
        {
            base.Awake();
            _throwingBullet = GetComponent<ThrowingBullet>();
            _triggerObserver = GetComponent<TriggerObserver>();
            SetInitialRotation(_throwingBullet);
            RigidBody.isKinematic = false;
        }

        private void OnEnable() =>
            _triggerObserver.CollisionEntered += OnCollisionEntered;

        private void OnDisable() =>
            _triggerObserver.CollisionEntered -= OnCollisionEntered;

        public override void Move(Vector3 target, Vector3 startPosition)
        {
            _moveDirection = target - startPosition;

            RigidBody.AddForce(_moveDirection.normalized * 35, ForceMode.Impulse);
            // RigidBody.AddTorque(transform.right * 50);
            transform.forward = _moveDirection;
            SetInitialRotation(_throwingBullet);
        }

        private void OnCollisionEntered(UnityEngine.Collision target)
        {
            transform.forward = _moveDirection;
            Vector3 startTargetRotation = new Vector3(104, transform.eulerAngles.y,
                transform.eulerAngles.z);
            // RigidBody.AddTorque(startTargetRotation);
            RigidBody.isKinematic = true;
            SetInitialRotation(_throwingBullet);

            if (target.gameObject.TryGetComponent(out EnemyPartForKnifeHolder enemyPartForKnifeHolder))
                transform.SetParent(target.transform);

            _isBlocked = true;
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