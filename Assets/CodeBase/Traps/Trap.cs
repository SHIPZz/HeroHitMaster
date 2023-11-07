using CodeBase.Constants;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
using UnityEngine;

namespace CodeBase.Traps
{
    [RequireComponent(typeof(TriggerObserver))]
    public abstract class Trap : MonoBehaviour
    {
        [field: SerializeField] public float DisableDelay { get; protected set; }
        protected Collider Collider;
        private TriggerObserver _triggerObserver;

        protected virtual void Awake()
        {
            _triggerObserver = GetComponent<TriggerObserver>();
            gameObject.layer = LayerId.Trap;
            Collider = GetComponent<Collider>();
        }

        private void OnEnable() =>
            _triggerObserver.Entered += Kill;

        private void OnDisable() =>
            _triggerObserver.Entered -= Kill;

        public abstract void Activate();

        protected virtual void Kill(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out Enemy enemy))
                enemy.Explode();
        }
    }
}