using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
using UnityEngine;

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