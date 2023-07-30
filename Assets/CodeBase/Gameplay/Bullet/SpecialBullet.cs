using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.MaterialChanger;
using Extensions;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public class SpecialBullet : Bullet, IBullet
    {
        [SerializeField] private Material _material;

        public void DoDamage(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IMaterialChanger materialChanger) ||
                !other.gameObject.TryGetComponent(out IDamageable damageable))
                return;


            materialChanger.Change(_material);
            damageable.TakeDamage(Damage);
            this.SetActive(gameObject, false, 0.2f);
        }
    }
}