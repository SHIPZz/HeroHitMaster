using CodeBase.Gameplay.Character.Players;
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
        private bool _isAttacked;

        [Inject]
        public void Construct(EnemyAttacker enemyAttacker,
            EnemyFollower enemyFollower,
            IProvider<PlayerProvider> playerProvider)
        {
            _playerProvider = playerProvider;
            _enemyAttacker = enemyAttacker;
            _enemyFollower = enemyFollower;
            _playerProvider.Get().Changed += SetPlayer;
        }

        private void Update()
        {
            if (_playerHealth is null)
                return;

            if (!(Vector3.Distance(transform.position, _playerHealth.transform.position) <
                  _targetStopDistance)) 
                return;

            if (_isAttacked)
                return;
            
            _enemyFollower.Block();
            _enemyAttacker.SetTarget(_playerHealth);
            _isAttacked = true;
        }
        
        private void OnDisable() => 
            _playerProvider.Get().Changed -= SetPlayer;

        private void SetPlayer(Player player) => 
            _playerHealth = player.GetComponent<PlayerHealth>();
    }
}