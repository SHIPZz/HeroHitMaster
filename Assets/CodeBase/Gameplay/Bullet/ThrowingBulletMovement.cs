using System;
using System.Collections;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Providers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class ThrowingBulletMovement : BulletMovement
    {
        [SerializeField] private Vector3 _bulletModelRotation;

        private TriggerObserver _triggerObserver;

        private ThrowingBullet _throwingBullet;
        private bool _isBlocked;
        private Vector3 _angularVelocity;
        private Transform _target;
        private bool _isSet;

        protected override void Awake()
        {
            base.Awake();
            _throwingBullet = GetComponent<ThrowingBullet>();
            _triggerObserver = GetComponent<TriggerObserver>();
            RigidBody.isKinematic = false;
        }

        private void OnEnable() =>
            _triggerObserver.Entered += OnTriggerEntered;

        private void OnDisable() =>
            _triggerObserver.Entered -= OnTriggerEntered;

        private void Update()
        {
            if (_target is null || !_isSet)
                return;

        }

        public override void Move(Vector3 target, Vector3 startPosition)
        {
            Vector3 moveDirection = target - startPosition;

            SetMove(moveDirection, startPosition);
            SetInitialRotation(_throwingBullet);
        }

        private void OnTriggerEntered(Collider target)
        {
            _target = target.transform;
            _isBlocked = true;
        }

        private void SetMove(Vector3 moveDirection, Vector3 startPosition)
        {
            transform.forward = moveDirection;
            transform.position = startPosition;

            if (MoveCoroutine != null)
                StopCoroutine(MoveCoroutine);

            MoveCoroutine = StartCoroutine(StartMoveCoroutine(moveDirection, 25));
        }

        private IEnumerator StartMoveCoroutine(Vector3 target, float speed)
        {
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(target.normalized);

            float elapsedTime = 0f;

            while (Vector3.Distance(transform.position, target) > 0.1)
            {
                if (_isBlocked)
                {
                    RigidBody.angularVelocity = Vector3.zero;
                    RigidBody.isKinematic = true;
                    RotateToTarget(elapsedTime, startRotation, targetRotation);
                    SetInitialRotation(_throwingBullet);

                    DOTween.Sequence().AppendInterval(0.1f)
                        .OnComplete(() =>
                        {
                            transform.SetParent(_target);
                            print(_target.name);
                            _isSet = true;
                        });
                    break;
                }

                CurrentVelocity = Vector3.Lerp(CurrentVelocity, target.normalized * speed, Time.deltaTime * 20);
                RigidBody.velocity = CurrentVelocity;
                _angularVelocity = Vector3.Lerp(_angularVelocity, transform.right * 80, Time.deltaTime * 80);
                RigidBody.angularVelocity = _angularVelocity;

                yield return new WaitForFixedUpdate();
            }
        }

        private void RotateToTarget(float elapsedTime, Quaternion startRotation, Quaternion targetRotation)
        {
            float time = elapsedTime / 0.3f;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, time);
        }

        private void SetInitialRotation(ThrowingBullet throwingBullet)
        {
            Vector3 startTargetRotation = new Vector3(104, transform.eulerAngles.y,
                transform.eulerAngles.z);

            RigidBody.DORotate(startTargetRotation, 0f);

            throwingBullet.BulletModel.DOLocalRotate(_bulletModelRotation, 0);
        }
    }
}