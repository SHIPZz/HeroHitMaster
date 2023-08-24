using System;
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
        private Vector3 _currentVelocity;
        private Coroutine _moveCoroutine;
        private Vector3 _angularVelocity;
        private Collider _target;

        public event Action Rotated;

        protected override void Awake()
        {
            base.Awake();
            _throwingBullet = GetComponent<ThrowingBullet>();
            _triggerObserver = GetComponent<TriggerObserver>();
            RigidBody.isKinematic = false;
        }

        private void OnEnable() => 
            _triggerObserver.Entered += OnTriggerObserverOnEntered;

        private void OnDisable() =>
            _triggerObserver.Entered -= OnTriggerObserverOnEntered;

        private void OnTriggerObserverOnEntered(Collider a)
        {
            _target = a;
            _isBlocked = true;
        }

        public override void Move(Vector3 target, Vector3 startPosition)
        {
            Vector3 moveDirection = target - startPosition;

            SetMove(moveDirection, startPosition);
            Rotate(_throwingBullet);
        }

        private void SetMove(Vector3 moveDirection, Vector3 startPosition)
        {
            transform.forward = moveDirection;
            transform.position = startPosition;

            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _moveCoroutine = StartCoroutine(StartMoveCoroutine(moveDirection, 25));
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
                    float t = elapsedTime / 0.3f;
                    transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
                    // transform.forward = target;
                    Rotate(_throwingBullet);
                    DOTween.Sequence().AppendInterval(0.3f).OnComplete(() => transform.SetParent(_target.transform));
                    break;
                }

                _currentVelocity = Vector3.Lerp(_currentVelocity, target.normalized * speed, Time.deltaTime * 20);
                RigidBody.velocity = _currentVelocity;
                _angularVelocity = Vector3.Lerp(_angularVelocity, transform.right * 80, Time.deltaTime * 80);
                RigidBody.angularVelocity = _angularVelocity;

                yield return new WaitForFixedUpdate();
            }
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