using CodeBase.Enums;
using CodeBase.Extensions;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Collision;
using CodeBase.Gameplay.MaterialChanger;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class SpecialBullet : MonoBehaviour, IBullet
    {
        [SerializeField] private Material _material;
        [SerializeField] private int Damage;

        [field: SerializeField] public BulletTypeId BulletTypeId { get; private set; }

        private TriggerObserver TriggerObserver;
        private GameObject _terrain;
        private float _distance;

        [Inject]
        private void Construct(TriggerObserver triggerObserver)
        {
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
            if (!other.gameObject.TryGetComponent(out IMaterialChanger materialChanger) ||
                !other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            materialChanger.Change(_material);
            damageable.TakeDamage(Damage);
            this.SetActive(gameObject, false, 0.2f);
        }
    }
}