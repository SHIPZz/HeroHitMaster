using System;
using CodeBase.Gameplay.Character;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet.DamageDealers
{
    public class ThrowingDamageDealer : DamageDealer
    {
        private Collider _collider;

        private void Awake() =>
            _collider = GetComponent<Collider>();

        private void OnEnable() =>
            _collider.enabled = true;

        public override void DoDamage(UnityEngine.Collision other)
        {
            if (other.gameObject.TryGetComponent(out EnemyPartForKnifeHolder enemyPartForKnifeHolder))
            {
                enemyPartForKnifeHolder.EnemyHealth.TakeDamage(Damage);
                _collider.enabled = false;
            }

            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(Damage);
                _collider.enabled = false;
            }

            // if (!enemyPartForKnifeHolder.EnemyHealth.isActiveAndEnabled)
            //     return;

            // _collider.enabled = false;
        }
    }
}