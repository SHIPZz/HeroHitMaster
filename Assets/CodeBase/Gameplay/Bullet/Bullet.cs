using CodeBase.Enums;
using CodeBase.Gameplay.Bullet.DamageDealers;
using CodeBase.Gameplay.Collision;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    [RequireComponent(typeof(DamageDealer))]
    public class Bullet : MonoBehaviour
    {
        [field: SerializeField] public WeaponTypeId WeaponTypeId { get; protected set; }

        private BulletMovement _bulletMovement;
        private TriggerObserver _triggerObserver;
        private DamageDealer _damageDealer;

        [Inject]
        private void Construct(TriggerObserver triggerObserver) => 
            _triggerObserver = triggerObserver;

        private void Awake()
        {
            _bulletMovement = GetComponent<BulletMovement>();
            _damageDealer = GetComponent<DamageDealer>();
        }

        private void OnEnable() =>
            _triggerObserver.CollisionEntered += DoDamage;

        private void OnDisable() =>
            _triggerObserver.CollisionEntered -= DoDamage;

        public void StartMovement(Vector3 target, Vector3 startPosition) =>
            _bulletMovement.Move(target, startPosition);

        private void DoDamage(UnityEngine.Collision collision) =>
            _damageDealer.DoDamage(collision);
    }
}