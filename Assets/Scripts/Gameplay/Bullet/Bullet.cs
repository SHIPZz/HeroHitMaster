using System.Collections.Generic;
using Enums;
using Extensions;
using Gameplay.Character;
using ScriptableObjects.WebSettings;
using UnityEngine;
using Zenject;

namespace Gameplay.Bullet
{
    public class Bullet : MonoBehaviour, IBullet
    {
        [field: SerializeField] public BulletTypeId BulletTypeId { get; protected set; }

        protected BulletSettings BulletSetting;
        protected List<BulletSettings> BulletSettingsList;

        [Inject]
        private void Construct(List<BulletSettings> bulletSettingsList)
        {
            BulletSettingsList = bulletSettingsList;
            Initialize();
        }

        public GameObject GameObject => gameObject;

        public Rigidbody Rigidbody => GetComponent<Rigidbody>();

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IDamageable damageable))
                return;
            
            damageable.TakeDamage(BulletSetting.Damage);
            this.SetActive(gameObject, false, .2f);
        }

        public virtual void Initialize()
        {
            BulletSetting = BulletSettingsList.Find(x => x.BulletTypeId == BulletTypeId);
            print(BulletSetting.Damage);
        }
    }
}