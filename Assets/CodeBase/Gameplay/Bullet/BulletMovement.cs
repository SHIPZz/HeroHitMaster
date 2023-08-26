using System.Collections;
using System.Collections.Generic;
using CodeBase.Services.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public  class BulletMovement : MonoBehaviour
    {
        protected Rigidbody RigidBody;
        protected float RotateDuration;
        protected float MoveDuration;
        protected Bullet Bullet;
        
        private BulletStaticDataService _bulletStaticDataService;
        protected Vector3 CurrentVelocity;
        protected Coroutine MoveCoroutine;

        [Inject]
        public void Construct(BulletStaticDataService bulletStaticDataService) =>
            _bulletStaticDataService = bulletStaticDataService;

        protected virtual void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            Bullet = GetComponent<Bullet>();
            RigidBody.isKinematic = false;
            RotateDuration = _bulletStaticDataService.GetBy(Bullet.WeaponTypeId).RotateDuration;
            MoveDuration = _bulletStaticDataService.GetBy(Bullet.WeaponTypeId).MovementDuration;
        }

        public virtual void Move(Vector3 target, Vector3 startPosition)
        {
            Vector3 direction = target - startPosition;

            if(MoveCoroutine != null)
                StopCoroutine(MoveCoroutine);
            
            MoveCoroutine = StartCoroutine(StartMoveCoroutine(direction, 50));
        }
        
        private IEnumerator StartMoveCoroutine(Vector3 target, float speed)
        {
            while (Vector3.Distance(transform.position, target) > 0.1)
            {
                CurrentVelocity = Vector3.Lerp(CurrentVelocity, target.normalized * speed, Time.deltaTime * 20);
                RigidBody.velocity = CurrentVelocity;

                yield return new WaitForFixedUpdate();
            }
        }
    }
}