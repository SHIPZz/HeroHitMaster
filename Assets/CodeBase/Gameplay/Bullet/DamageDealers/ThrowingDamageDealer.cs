using System;
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
            if (!other.gameObject.TryGetComponent(out EnemyPartForKnifeHolder enemyPartForKnifeHolder))
                return;
            
            if (!enemyPartForKnifeHolder.EnemyHealth.isActiveAndEnabled)
                return;

            enemyPartForKnifeHolder.EnemyHealth.TakeDamage(Damage);
            _collider.enabled = false;
        }
    }
}