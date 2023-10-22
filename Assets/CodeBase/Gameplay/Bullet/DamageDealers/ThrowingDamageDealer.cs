using System;
using CodeBase.Constants;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Character.Enemy;
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
                enemyPartForKnifeHolder.Damageable.TakeDamage(Damage);
                _collider.enabled = false;
            }
            
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(Damage);
                _collider.enabled = false;
            }
            
            if(other.gameObject.layer == LayerId.DestroyableObject)
                gameObject.SetActive(false);

            gameObject.layer = LayerId.HitBullet;
        }
    }
}