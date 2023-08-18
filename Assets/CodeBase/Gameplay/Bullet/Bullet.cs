using System;
using CodeBase.Enums;
using CodeBase.Extensions;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class Bullet : MonoBehaviour, IBullet
    {
        [field: SerializeField] public BulletTypeId BulletTypeId { get; private set; }
        
        private TriggerObserver _triggerObserver;
        private GameObject _terrain;
        private float _distance;
        private int _damage;
        private IBulletMovement _bulletMovement;

        [Inject]
        private void Construct(TriggerObserver triggerObserver, BulletStaticDataService bulletStaticDataService)
        {
            _triggerObserver = triggerObserver;
            _damage = bulletStaticDataService.GetBy(BulletTypeId).Damage;
        }

        public GameObject GameObject => gameObject;

        public Rigidbody Rigidbody => GetComponent<Rigidbody>();

        private void Awake() => 
            _bulletMovement = GetComponent<IBulletMovement>();

        private  void OnEnable() => 
            _triggerObserver.Entered += DoDamage;

        private void OnDisable() => 
            _triggerObserver.Entered -= DoDamage;

        public void DoDamage(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            damageable.TakeDamage(_damage);
            this.SetActive(gameObject, false, 0.5f);
        }

        public void Move(Vector3 target, Vector3 startPosition) => 
            _bulletMovement.Move(target,this,startPosition,Rigidbody);
    }
}