namespace Gameplay.Character.Enemy
{
    public class EnemyAttacker
    { 
        private const int Damage = 1000;

        private readonly EnemyAnimator _enemyAnimator;
        
        public EnemyAttacker(EnemyAnimator enemyAnimator) => 
            _enemyAnimator = enemyAnimator;

        public void Attack(IDamageable damageable)
        {
            _enemyAnimator.SetAttack(true);
            damageable.TakeDamage(Damage);
        }
    }
}