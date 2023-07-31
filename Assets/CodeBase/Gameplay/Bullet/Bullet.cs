using CodeBase.Enums;
using CodeBase.Extensions;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Collision;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class Bullet : MonoBehaviour, IBullet
    {
        [SerializeField] protected int Damage;

        [field: SerializeField] public BulletTypeId BulletTypeId { get; protected set; }
        
        private TriggerObserver _triggerObserver;
        private GameObject _terrain;
        private float _distance;

        [Inject]
        private void Construct(TriggerObserver triggerObserver)
        {
            _triggerObserver = triggerObserver;
        }

        public GameObject GameObject => gameObject;

        public Rigidbody Rigidbody => GetComponent<Rigidbody>();

        private  void OnEnable() => 
            _triggerObserver.Entered += DoDamage;

        private void OnDisable() => 
            _triggerObserver.Entered -= DoDamage;

        public void DoDamage(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            damageable.TakeDamage(Damage);
            this.SetActive(gameObject, false, 0.5f);
        }
    }
}