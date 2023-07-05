using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
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
        [field: SerializeField] public BulletTypeId BulletTypeId { get; private set; }

        [SerializeField] private Material _material;

        private BulletSettings _bulletSetting;
        private Transform _target;

        [Inject]
        private void Construct(List<BulletSettings> bulletSettingsList) =>
            _bulletSetting = bulletSettingsList.Find(x => x.BulletTypeId == BulletTypeId);

        public GameObject GameObject => gameObject;

        public Rigidbody Rigidbody => GetComponent<Rigidbody>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IViewChangeable viewChangeable))
            {
                viewChangeable.SetMaterial(_material);
                this.SetActive(gameObject,  false,.2f);
            }

            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_bulletSetting.Damage);
                this.SetActive(gameObject,  false,.2f);
            }
        }
    }
}