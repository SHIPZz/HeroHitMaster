using System;
using System.Collections;
using CodeBase.Gameplay.Collision;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public class ThrowingBulletMovement : BulletMovement
    {
        [SerializeField] private Vector3 _bulletModelRotation;
        private TriggerObserver _triggerObserver;

        private ThrowingBullet _throwingBullet;
        private bool _isBlocked;
        private Vector3 _currentVelocity;
        private Coroutine _moveCoroutine;
        private Tweener _tweener;
        private Vector3 _angularVelocity;

        protected override void Awake()
        {
            base.Awake();
            _throwingBullet = GetComponent<ThrowingBullet>();
            RigidBody.isKinematic = false;
            _triggerObserver = GetComponent<TriggerObserver>();
            _triggerObserver.Entered += OnTriggerObserverOnEntered;
        }

        private void OnTriggerObserverOnEntered(Collider a)
        {
            _isBlocked = true;
        }

        public override void Move(Vector3 target, Vector3 startPosition)
        {
            var moveDirection = target - startPosition;

            SetMove(moveDirection, target, startPosition);
            Rotate(_throwingBullet, RotateDuration);
        }

        private void SetMove(Vector3 moveDirection, Vector3 target, Vector3 startPosition)
        {
            transform.forward = moveDirection;
            transform.position = startPosition;

            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _moveCoroutine = StartCoroutine(StartMoveCoroutine(moveDirection, 25));
        }

        private IEnumerator StartMoveCoroutine(Vector3 target, float speed)
        {
            while (Vector3.Distance(transform.position, target) > 0.1)
            {
                if (_isBlocked)
                {
                    RigidBody.angularVelocity = Vector3.zero;
                    RigidBody.isKinematic = true;
                    transform.forward = target;
                    Rotate(_throwingBullet);
                    break;
                }

                _currentVelocity = Vector3.Lerp(_currentVelocity, target.normalized * speed, Time.deltaTime * 20);
                RigidBody.velocity = _currentVelocity;
                _angularVelocity = Vector3.Lerp(_angularVelocity, transform.right * 80, Time.deltaTime * 80);
                RigidBody.angularVelocity = _angularVelocity;
                // RigidBody.AddTorque(_angularVelocity);

                yield return new WaitForFixedUpdate();
            }
        }

        private void Rotate(ThrowingBullet throwingBullet, float rotateDuration)
        {
            Rotate(throwingBullet);

            // Vector3 flyRotation = new Vector3(360, 0, 0);
            // _tweener = RigidBody.DORotate(flyRotation, rotateDuration).SetRelative(true).SetEase(Ease.Linear);
        }

        private void Rotate(ThrowingBullet throwingBullet)
        {
            Vector3 startTargetRotation = new Vector3(104, transform.eulerAngles.y,
                transform.eulerAngles.z);

            RigidBody.DORotate(startTargetRotation, 0f);

            throwingBullet.BulletModel.DOLocalRotate(_bulletModelRotation, 0);
        }
    }
}