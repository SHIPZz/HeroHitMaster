using System;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyState : MonoBehaviour
    {
        [SerializeField] private float _targetStopDistance;
        
        private EnemyAttacker _enemyAttacker;
        private EnemyFollower _enemyFollower;
        private TriggerObserver _triggerObserver;
        private PlayerProvider _playerProvider;

        [Inject]
        public void Construct(EnemyAttacker enemyAttacker, EnemyFollower enemyFollower, TriggerObserver triggerObserver,
            PlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
            _enemyAttacker = enemyAttacker;
            _enemyFollower = enemyFollower;
            _triggerObserver = triggerObserver;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _playerProvider.CurrentPlayer.transform.position) <
                _targetStopDistance)
            {
                _enemyFollower.Block();
                _enemyAttacker.Attack(_playerProvider.PlayerHealth);
            }
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