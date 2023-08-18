using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Extensions;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Collision;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class SpecialBullet : MonoBehaviour, IBullet
    {
        [SerializeField] private MaterialTypeId _materialTypeId;

        [field: SerializeField] public BulletTypeId BulletTypeId { get; private set; }

        private int _damage;
        private Material _material;
        private TriggerObserver TriggerObserver;
        private float _distance;
        private IBulletMovement _bulletMovement;

        [Inject]
        private void Construct(TriggerObserver triggerObserver,
            IProvider<MaterialTypeId, Material> materialProvider,
            BulletStaticDataService bulletStaticDataService)
        {
            _material = materialProvider.Get(_materialTypeId);
            _damage = bulletStaticDataService.GetBy(BulletTypeId).Damage;
            TriggerObserver = triggerObserver;
        }

        private void Awake() => 
            _bulletMovement = GetComponent<IBulletMovement>();

        public GameObject GameObject => gameObject;

        public Rigidbody Rigidbody => GetComponent<Rigidbody>();

        private void OnEnable() =>
            TriggerObserver.Entered += DoDamage;

        private void OnDisable() =>
            TriggerObserver.Entered -= DoDamage;
        
        public void Move(Vector3 target,Vector3 startPosition )
        {
            _bulletMovement.Move(target,this,startPosition, Rigidbody);
        }


        public void DoDamage(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IMaterialChanger materialChanger))
            {
                materialChanger.Change(_material);
                this.SetActive(gameObject, false, 0.1f);
            }

            if (!other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            if (other.gameObject.TryGetComponent(out Animator animator))
                animator.enabled = false;

            damageable.TakeDamage(_damage);
            this.SetActive(gameObject, false, 0.1f);
        }
    }
}