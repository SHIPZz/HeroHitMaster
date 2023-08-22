using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public class ThrowingBulletMovement : BulletMovement
    {
        [SerializeField] private Vector3 _bulletModelRotation;
        
        private ThrowingBullet _throwingBullet;

        private Vector3 _targetPosition;
        private Vector3 _startPosition;
        private Vector3 _moveDirection;

        private bool _isMoving;

        protected override void Awake()
        {
            base.Awake();
            _throwingBullet = GetComponent<ThrowingBullet>();
            RigidBody.isKinematic = false;
        }

        private void FixedUpdate()
        {
            if (!_isMoving)
                return;
            
            Vector3 velocity = _moveDirection.normalized * 30;
            RigidBody.velocity = velocity;
        }

        private void OnDisable()
        {
            _isMoving = false;
        }

        public override void Move(Vector3 target, Vector3 startPosition)
        {
            _targetPosition = target;
            _startPosition = startPosition;
            _moveDirection = _targetPosition - _startPosition;
            
            transform.forward = target;
            transform.position = startPosition;
            
            Rotate(_throwingBullet, RotateDuration);
            
            _isMoving = true;
        }

        private void Rotate(ThrowingBullet throwingBullet, float rotateDuration)
        {
            Vector3 startTargetRotation = new Vector3(104, transform.localEulerAngles.y,
                transform.localEulerAngles.z);

            transform.DORotate(startTargetRotation, 0f);

            Vector3 flyRotation = new Vector3(360, 0, 0);

            throwingBullet.BulletModel.DOLocalRotate(_bulletModelRotation, 0);

            // transform.DORotate(flyRotation, rotateDuration).SetRelative(true).SetEase(Ease.Linear);
        }
    }
}
