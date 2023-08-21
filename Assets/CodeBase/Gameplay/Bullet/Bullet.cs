using System;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Extensions;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Data;
using DG.Tweening;
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
        private bool _isHit;

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

        private void OnEnable()
        {
            _triggerObserver.Entered += DoDamage;
            _isHit = false;
        }

        private void OnDisable()
        {
            _triggerObserver.Entered -= DoDamage;
        }

        public void DoDamage(Collider other)
        {
            if(_isHit)
                return;

            _isHit = true;
            
            if (other.gameObject.TryGetComponent(out EnemyPartForKnifeHolder enemyPartForKnifeHolder))
            {
                transform.SetParent(enemyPartForKnifeHolder.Head);
                print(enemyPartForKnifeHolder.name);
            }

            if (!other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            damageable.TakeDamage(_damage);
        }

        public void Move(Vector3 target, Vector3 startPosition) =>
            _bulletMovement.Move(target, this, startPosition, Rigidbody);
    }
}