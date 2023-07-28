using Enums;
using Gameplay.Character.Enemy;
using Services.Factories;
using UnityEngine;
using Zenject;

namespace Gameplay.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        [SerializeField] private TriggerObserver _aggroZone;

        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        private void Awake()
        {
            Enemy enemy = _enemyFactory.CreateBy(_enemyTypeId, transform.position);
            var enemyFollower = enemy.GetComponent<EnemyFollower>();
            enemyFollower.SetAggroZone(_aggroZone);
        }
    }
}