using CodeBase.Gameplay.Collision;
using Enums;
using Extensions;
using Gameplay.Character;
using UnityEngine;
using Zenject;

namespace Gameplay.Bullet
{
    public class Bullet : MonoBehaviour, IBullet
    {
        [SerializeField] protected int Damage;

        [field: SerializeField] public BulletTypeId BulletTypeId { get; protected set; }
        protected TriggerObserver TriggerObserver;
        private GameObject _terrain;
        private float _distance;

        [Inject]
        private void Construct(TriggerObserver triggerObserver)
        {
            TriggerObserver = triggerObserver;
        }

        public GameObject GameObject => gameObject;

        public Rigidbody Rigidbody => GetComponent<Rigidbody>();

        private  void OnEnable() => 
            TriggerObserver.Entered += DoDamage;

        private void OnDisable() => 
            TriggerObserver.Entered -= DoDamage;

        protected virtual void DoDamage(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            damageable.TakeDamage(Damage);
            this.SetActive(gameObject, false, 0.5f);
        }
    }
}