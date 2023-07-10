using System;
using DG.Tweening;
using Enums;
using Extensions;
using Gameplay.Character;
using UnityEngine;
using Zenject;

namespace Gameplay.Bullet
{
    public class Bullet : MonoBehaviour, IInitializable, IDisposable, ITickable, IBullet
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
            Initialize();
        }

        public GameObject GameObject => gameObject;

        public Rigidbody Rigidbody => GetComponent<Rigidbody>();

        public virtual void Initialize() =>
            TriggerObserver.Entered += DoDamage;

        public virtual void Dispose() =>
            TriggerObserver.Entered -= DoDamage;

        protected virtual void DoDamage(Collider other)
        {
            // if (other.gameObject.tag == "Terrain")
            // {
            //     gameObject.transform.DORotate(new Vector3(90, 0, 0), 0.5f);
            // }

            if (!other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            damageable.TakeDamage(Damage);
            this.SetActive(gameObject, false, 0.2f);
        }

        public void Tick()
        {
            // RaycastHit hit;
            //
            // if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, 100f))
            // {
            //     _distance = hit.distance;
            //     print(_distance);
            //     
            //     if(_distance < 0.1)
            //         gameObject.transform.DORotate(new Vector3(90, 0, 0), 0.1f);
            // }
        }
    }
}