using CodeBase.Enums;
using CodeBase.Services.Data;
using CodeBase.Services.Storages;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyAttacker
    { 
        private readonly int _damage;
        private readonly EnemyAnimator _enemyAnimator;
        private IDamageable _damageable;

        public EnemyAttacker(EnemyAnimator enemyAnimator , EnemyTypeId enemyTypeId, EnemyStaticDataService enemyStaticDataService)
        {
            _enemyAnimator = enemyAnimator;
            _damage = enemyStaticDataService.GetEnemyData(enemyTypeId).Damage;
        }

        public void Attack() => 
        _damageable.TakeDamage(_damage);

        public void SetTarget(IDamageable damageable)
        {
            _enemyAnimator.SetAttack(true);
            _damageable = damageable;
        }
    }
}