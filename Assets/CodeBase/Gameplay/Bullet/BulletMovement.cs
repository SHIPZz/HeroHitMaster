using CodeBase.Services.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public abstract class BulletMovement : MonoBehaviour
    {
        protected Rigidbody RigidBody;
        protected float RotateDuration;
        protected float MoveDuration;
        protected Bullet Bullet;
        
        private BulletStaticDataService _bulletStaticDataService;
        
        [Inject]
        public void Construct(BulletStaticDataService bulletStaticDataService) =>
            _bulletStaticDataService = bulletStaticDataService;

        protected virtual void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            Bullet = GetComponent<Bullet>();
            RotateDuration = _bulletStaticDataService.GetBy(Bullet.BulletTypeId).RotateDuration;
            MoveDuration = _bulletStaticDataService.GetBy(Bullet.BulletTypeId).MovementDuration;
        }

        public abstract void Move(Vector3 target, Vector3 startPosition);
    }
}