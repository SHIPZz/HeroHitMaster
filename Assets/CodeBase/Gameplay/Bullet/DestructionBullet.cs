using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Collision;
using CodeBase.Gameplay.ObjectBodyPart;
using CodeBase.Services.Data;
using CodeBase.Services.Storages;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class DestructionBullet : MonoBehaviour, IBullet
    {
        [field: SerializeField] public BulletTypeId BulletTypeId { get; private set; }

        private int _damage;
        private TriggerObserver _triggerObserver;
        private IBulletMovement _bulletMovement;

        public GameObject GameObject => gameObject;
        public Rigidbody Rigidbody { get; private set; }

        [Inject]
        private void Construct(TriggerObserver triggerObserver, BulletStaticDataService bulletStaticDataService)
        {
            _triggerObserver = triggerObserver;
            _damage = bulletStaticDataService.GetBy(BulletTypeId).Damage;
        }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            _bulletMovement = GetComponent<IBulletMovement>();
        }

        private void OnEnable()
        {
            _triggerObserver.Entered += DoDamage;
        }

        private void OnDisable()
        {
            _triggerObserver.Entered -= DoDamage;
        }

        public void Move(Vector3 target,Vector3 startPosition )
        {
            _bulletMovement.Move(target,this,startPosition, Rigidbody);
        }

        public void DoDamage(Collider other)
        {
            if (other.TryGetComponent(out Animator animator))
                animator.enabled = false;
            
            if(other.TryGetComponent(out IDestroyable destroyable))
                destroyable.Destroy();
            
            if(other.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damage);
            
            gameObject.SetActive(false);
        }
    }
}