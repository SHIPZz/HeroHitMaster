using System;
using Enums;
using Extensions;
using Gameplay.Character;
using UnityEngine;
using Zenject;

namespace Gameplay.Bullet
{
    public class Bullet : MonoBehaviour, IInitializable, IDisposable, IBullet
    {
        [SerializeField] protected int Damage;
        
        [field: SerializeField] public BulletTypeId BulletTypeId { get; protected set; }

        protected TriggerObserver TriggerObserver;

        [Inject]
        private void Construct(TriggerObserver triggerObserver)
        {
            TriggerObserver = triggerObserver;
            Initialize();
        }

        public GameObject GameObject => gameObject;

        public Rigidbody Rigidbody => GetComponent<Rigidbody>();

        public virtual void Initialize()
        {
            TriggerObserver.Entered += DoDamage;
        }

        public virtual void Dispose() =>
            TriggerObserver.Entered -= DoDamage;

        protected virtual void DoDamage(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            damageable.TakeDamage(Damage);
            this.SetActive(gameObject,false,0.2f);
        }
    }
}