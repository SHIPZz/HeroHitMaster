using CodeBase.Gameplay.Collision;
using Gameplay.Character.Enemy;

namespace CodeBase.Gameplay.Spawners
{
    public class EnemyConfigurator
    {
        public void Configure(Enemy enemy, TriggerObserver aggroZone)
        {
            var enemyFollower = enemy.GetComponent<EnemyFollower>();
            enemyFollower.SetAggroZone(aggroZone);
            enemyFollower.gameObject.SetActive(true);
        }
    }
}