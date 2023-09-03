using System;
using System.Collections.Generic;
using CodeBase.Constants;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.ObjectBodyPart;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet.DamageDealers
{
    public class DestructionDamageDealer : DamageDealer
    {
        public event Action Done;

        private List<int> _layersToNotDisableObject = new()
        {
            LayerId.Floor,
            LayerId.HardCube,
            LayerId.Wall,
            LayerId.Water,
        };

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
            {
                damageable.TakeDamage(Damage);
            }

            if (!_layersToNotDisableObject.Contains(other.gameObject.layer))
                gameObject.SetActive(false);
        }
    }
}