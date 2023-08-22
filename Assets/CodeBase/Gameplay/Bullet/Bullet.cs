using CodeBase.Enums;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public abstract class Bullet : MonoBehaviour
    {
        [field: SerializeField] public BulletTypeId BulletTypeId { get; protected set; }

        protected BulletMovement BulletMovement;
        protected int Damage;
        private TriggerObserver _triggerObserver;

        [Inject]
        private void Construct(TriggerObserver triggerObserver, BulletStaticDataService bulletStaticDataService)
        {
            _triggerObserver = triggerObserver;
            Damage = bulletStaticDataService.GetBy(BulletTypeId).Damage;
        }

        private void Awake() =>
            BulletMovement = GetComponent<BulletMovement>();

        private void OnEnable() =>
            _triggerObserver.Entered += DoDamage;

        private void OnDisable() =>
            _triggerObserver.Entered -= DoDamage;

        public void Move(Vector3 target, Vector3 startPosition) =>
            BulletMovement.Move(target, startPosition);

        protected abstract void DoDamage(Collider other);
    }
}