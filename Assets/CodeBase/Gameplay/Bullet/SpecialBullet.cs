using CodeBase.Enums;
using CodeBase.Extensions;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Collision;
using CodeBase.Gameplay.MaterialChanger;
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
        private GameObject _terrain;
        private float _distance;

        [Inject]
        private void Construct(TriggerObserver triggerObserver, MaterialProvider materialProvider,BulletStaticDataService bulletStaticDataService)
        {
            _material = materialProvider.Materials[_materialTypeId];
            _damage = bulletStaticDataService.GetBy(BulletTypeId).Damage;
            TriggerObserver = triggerObserver;
        }

        public GameObject GameObject => gameObject;

        public Rigidbody Rigidbody => GetComponent<Rigidbody>();

        private void OnEnable() =>
            TriggerObserver.Entered += DoDamage;

        private void OnDisable() =>
            TriggerObserver.Entered -= DoDamage;

        public void DoDamage(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IMaterialChanger materialChanger))
            {
                materialChanger.Change(_material);
                this.SetActive(gameObject, false, 0.1f);
            }

            if (!other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            damageable.TakeDamage(_damage);
            this.SetActive(gameObject, false, 0.1f);
        }
    }
}