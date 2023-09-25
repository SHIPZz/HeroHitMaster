using CodeBase.Gameplay.Character.Players;
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
        private IProvider<PlayerProvider> _playerProvider;
        private PlayerHealth _playerHealth;

        [Inject]
        public void Construct(EnemyAttacker enemyAttacker, EnemyFollower enemyFollower)
        {
            _enemyAttacker = enemyAttacker;
            _enemyFollower = enemyFollower;
        }

        private void Update()
        {
            if (_playerHealth is null)
                return;

            if (!(Vector3.Distance(transform.position, _playerHealth.transform.position) <
                  _targetStopDistance)) 
                return;
                
            _enemyFollower.Block();
            _enemyAttacker.SetTarget(_playerHealth);
        }
    }
}