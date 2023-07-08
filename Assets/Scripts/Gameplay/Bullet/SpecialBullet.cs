using System;
using Extensions;
using Gameplay.Character;
using Gameplay.MaterialChanger;
using UnityEngine;

namespace Gameplay.Bullet
{
    public class SpecialBullet : Bullet
    {
        private const int DestroyDamage = 1000;

        [SerializeField] private Material _material;

        public override void Initialize()
        {
            TriggerObserver.Entered += DoDamage;
            Damage = DestroyDamage;
        }

        public override void Dispose() => 
            TriggerObserver.Entered -= DoDamage;

        protected override void DoDamage(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IMaterialChanger materialChanger) ||
                !other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            materialChanger.Change(_material);
            damageable.TakeDamage(Damage);
            this.SetActive(gameObject,false,0.2f);
        }
    }
}

