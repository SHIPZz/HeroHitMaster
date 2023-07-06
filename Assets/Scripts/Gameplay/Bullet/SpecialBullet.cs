using Gameplay.Character;
using Gameplay.MaterialChanger;
using UnityEngine;

namespace Gameplay.Bullet
{
    public class SpecialBullet : Bullet
    {
        private const int DestroyDamage = 1000;

        [SerializeField] private Material _material;

        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IMaterialChanger materialChanger) ||
                !other.gameObject.TryGetComponent(out IDamageable damageable))
                return;

            materialChanger.Change(_material);
            damageable.TakeDamage(DestroyDamage);
        }
    }
}