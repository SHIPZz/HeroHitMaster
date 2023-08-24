using UnityEngine;

namespace CodeBase.Gameplay.Bullet.DamageDealers
{
    public class ThrowingDamageDealer : DamageDealer
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public override void DoDamage(Collider other)
        {
            if (other.TryGetComponent(out EnemyPartForKnifeHolder enemyPartForKnifeHolder))
            {
                if(!enemyPartForKnifeHolder.EnemyHealth.isActiveAndEnabled)
                    return;
                
                enemyPartForKnifeHolder.EnemyHealth.TakeDamage(Damage);
                _collider.enabled = false;
            }
        }
    }
}