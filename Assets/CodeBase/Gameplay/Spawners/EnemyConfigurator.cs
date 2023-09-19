using CodeBase.Gameplay.Character.Enemy;
using UnityEngine.AI;

namespace CodeBase.Gameplay.Spawners
{
    public class EnemyConfigurator
    {
        public void Configure(Enemy enemy, AggroZone aggroZone)
        {
            var enemyFollower = enemy.GetComponent<EnemyFollower>();
            var navmeshAgent = enemy.GetComponent<NavMeshAgent>();
            navmeshAgent.enabled = true;
            enemyFollower.SetAggroZone(aggroZone);
        }
    }
}