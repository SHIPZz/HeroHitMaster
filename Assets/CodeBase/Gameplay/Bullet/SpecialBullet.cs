using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Extensions;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Collision;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class SpecialBullet : Bullet
    {
        [SerializeField] private MaterialTypeId _materialTypeId;

        private Material _material;

        [Inject]
        private void Construct(IProvider<MaterialTypeId, Material> materialProvider) => 
            _material = materialProvider.Get(_materialTypeId);

        protected override void DoDamage(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IMaterialChanger materialChanger))
            {
                materialChanger.Change(_material);
                this.SetActive(gameObject, false, 0.1f);
            }

            if (!other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            if (other.gameObject.TryGetComponent(out Animator animator))
                animator.enabled = false;

            damageable.TakeDamage(Damage);
            this.SetActive(gameObject, false, 0.1f);
        }
    }
}