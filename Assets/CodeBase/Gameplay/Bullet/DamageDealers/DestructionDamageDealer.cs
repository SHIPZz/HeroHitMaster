using System;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.ObjectBodyPart;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet.DamageDealers
{
    public class DestructionDamageDealer : DamageDealer
    {
        public event Action Done;

        public override void DoDamage(UnityEngine.Collision other)
        {
            if (other.gameObject.TryGetComponent(out Animator animator))
                animator.enabled = false;

            if (other.gameObject.TryGetComponent(out EnemyPartForKnifeHolder enemyPartForKnifeHolder))
            {
                enemyPartForKnifeHolder.Enemy.Explode();
                Done?.Invoke();
            }

            if (other.gameObject.TryGetComponent(out IExplodable destroyable))
            {
                destroyable.Explode();
                Done?.Invoke();
            }

            if (other.gameObject.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(Damage);

            // gameObject.SetActive(false);
        }
    }
}