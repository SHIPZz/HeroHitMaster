using CodeBase.Enums;
using CodeBase.Extensions;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet.DamageDealers
{
    public class SpecialDamageDealer : DamageDealer
    {
        [SerializeField] private MaterialTypeId _materialTypeId;

        private Material _material;

        [Inject]
        private void Construct(IProvider<MaterialTypeId, Material> materialProvider) =>
            _material = materialProvider.Get(_materialTypeId);

        public override void DoDamage(UnityEngine.Collision other)
        {
            if (other.gameObject.TryGetComponent(out IMaterialChanger materialChanger))
            {
                materialChanger.Change(_material);
                this.SetActive(gameObject, false, 0.1f);
            }

            if (other.gameObject.TryGetComponent(out EnemyPartForKnifeHolder enemyPartForKnifeHolder))
            {
                enemyPartForKnifeHolder.MaterialChanger.Change(_material);
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