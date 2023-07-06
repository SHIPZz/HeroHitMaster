using System;
using System.Collections.Generic;
using Enums;
using Extensions;
using Gameplay.Character;
using ScriptableObjects.WebSettings;
using UnityEngine;
using Zenject;

namespace Gameplay.Bullet
{
    public class Bullet : MonoBehaviour, IInitializable, IDisposable, IBullet
    {
        [field: SerializeField] public BulletTypeId BulletTypeId { get; protected set; }

        protected TriggerObserver TriggerObserver;
        protected BulletSettings BulletSetting;
        protected List<BulletSettings> BulletSettingsList;

        [Inject]
        private void Construct(List<BulletSettings> bulletSettingsList, TriggerObserver triggerObserver)
        {
            TriggerObserver = triggerObserver;
            BulletSettingsList = bulletSettingsList;
            Initialize();
        }

        public GameObject GameObject => gameObject;

        public Rigidbody Rigidbody => GetComponent<Rigidbody>();

        public virtual void Initialize()
        {
            BulletSetting = BulletSettingsList.Find(x => x.BulletTypeId == BulletTypeId);
            TriggerObserver.Entered += DoDamage;
        }

        public virtual void Dispose() =>
            TriggerObserver.Entered -= DoDamage;

        protected virtual void DoDamage(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            damageable.TakeDamage(BulletSetting.Damage);
            this.SetActive(gameObject,false,0.2f);
        }
    }
}