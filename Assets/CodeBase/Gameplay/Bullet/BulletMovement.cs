using System;
using System.Collections;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class BulletMovement : MonoBehaviour
    {
        protected Rigidbody RigidBody;
        protected Bullet Bullet;

        private BulletStaticDataService _bulletStaticDataService;
        protected TriggerObserver TriggerObserver;
        protected Vector3 MoveDirection;
        protected bool IsHit;

        [Inject]
        public void Construct(BulletStaticDataService bulletStaticDataService) =>
            _bulletStaticDataService = bulletStaticDataService;

        protected virtual void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            Bullet = GetComponent<Bullet>();
            TriggerObserver = GetComponent<TriggerObserver>();
            RigidBody.isKinematic = false;
        }

        private void OnEnable()
        {
            TriggerObserver.CollisionEntered += OnCollisionEntered;
        }

        private void FixedUpdate()
        {
            if (MoveDirection == Vector3.zero)
                return;

            if (IsHit)
            {
                RigidBody.velocity = Vector3.zero;
                RigidBody.angularVelocity = Vector3.zero;
                return;
            }

            RigidBody.velocity = MoveDirection * 35;
            RigidBody.angularVelocity = transform.right * 50;
        }

        private void OnDisable()
        {
            TriggerObserver.CollisionEntered -= OnCollisionEntered;
            IsHit = false;
            RigidBody.useGravity = true;
        }

        protected virtual void OnCollisionEntered(UnityEngine.Collision collision)
        {
            transform.forward = MoveDirection;
            RigidBody.useGravity = false;
            RigidBody.velocity = Vector3.zero;
            RigidBody.angularVelocity = Vector3.zero;
            IsHit = true;
        }

        public virtual void Move(Vector3 target, Vector3 startPosition)
        {
            MoveDirection = target - startPosition;
            MoveDirection.Normalize();
        }
    }
}