using CodeBase.Gameplay.Collision;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyState : MonoBehaviour
    {
        private EnemyAttacker _enemyAttacker;
        private EnemyFollower _enemyFollower;
        private TriggerObserver _triggerObserver;

        [Inject]
        public void Construct(EnemyAttacker enemyAttacker, EnemyFollower enemyFollower, TriggerObserver triggerObserver)
        {
            _enemyAttacker = enemyAttacker;
            _enemyFollower = enemyFollower;
            _triggerObserver = triggerObserver;
        }

        private void OnEnable()
        {
            _triggerObserver.Entered += PlayerApproached;
        }

        private void OnDisable()
        {
            _triggerObserver.Entered -= PlayerApproached;
        }
        private void PlayerApproached(Collider obj)
        {
            if (obj.gameObject.TryGetComponent(out IDamageable damageable))
            {
                Debug.Log("sadasda");
                _enemyFollower.Block();
                _enemyAttacker.Attack(damageable);
            }
        }
    }
}