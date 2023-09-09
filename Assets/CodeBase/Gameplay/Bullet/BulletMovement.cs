using System;
using System.Collections;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField] private bool _needBlockRotation;

        protected Rigidbody RigidBody;
        protected Bullet Bullet;

        protected BulletStaticDataService BulletStaticDataService;
        protected TriggerObserver TriggerObserver;
        protected Vector3 MoveDirection;
        protected float RotateSpeed;
        protected float MoveSpeed;
        protected bool IsHit;

        [Inject]
        public void Construct(BulletStaticDataService bulletStaticDataService) =>
            BulletStaticDataService = bulletStaticDataService;

        protected virtual void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            Bullet = GetComponent<Bullet>();
            TriggerObserver = GetComponent<TriggerObserver>();
            RotateSpeed = BulletStaticDataService.GetBy(Bullet.WeaponTypeId).RotationSpeed;
            MoveSpeed = BulletStaticDataService.GetBy(Bullet.WeaponTypeId).Speed;
        }

        private void OnEnable() => 
            TriggerObserver.CollisionEntered += OnCollisionEntered;

        protected void FixedUpdate()
        {
            if (IsHit)
            {
                RigidBody.velocity = Vector3.zero;
                RigidBody.angularVelocity = Vector3.zero;
                return;
            }

            if (_needBlockRotation == false)
                RigidBody.angularVelocity = transform.right * RotateSpeed;

            RigidBody.velocity = MoveDirection * MoveSpeed;
        }

        private void OnDisable()
        {
            TriggerObserver.CollisionEntered -= OnCollisionEntered;
            IsHit = false;
            RigidBody.useGravity = true;
        }

        protected virtual void OnCollisionEntered(UnityEngine.Collision collision)
        {
            Vector3 offset = collision.GetContact(0).point - transform.position;
            transform.position += offset;
            transform.forward = MoveDirection;

            RigidBody.useGravity = false;
            RigidBody.velocity = Vector3.zero;
            RigidBody.angularVelocity = Vector3.zero;
            IsHit = true;
        }

        public virtual void Move(Vector3 target, Vector3 startPosition)
        {
            RigidBody.position = startPosition;
            transform.position = startPosition;
            MoveDirection = target - startPosition;
            MoveDirection = MoveDirection.normalized;
            transform.forward = MoveDirection;
        }
    }
}