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
        private TriggerObserver _triggerObserver;
        private IProvider<Player> _playerProvider;

        [Inject]
        public void Construct(EnemyAttacker enemyAttacker,
            EnemyFollower enemyFollower, 
            TriggerObserver triggerObserver,
            IProvider<Player> playerProvider)
        {
            _playerProvider = playerProvider;
            _enemyAttacker = enemyAttacker;
            _enemyFollower = enemyFollower;
            _triggerObserver = triggerObserver;
        }

        private void Update()
        {
            if (_playerProvider.Get() is null)
                return;
            
            if (!(Vector3.Distance(transform.position, _playerProvider.Get().transform.position) <
                  _targetStopDistance))
                return;
            
            _enemyFollower.Block();
            _enemyAttacker.SetTarget(_playerProvider.Get().GetComponent<PlayerHealth>());
        }

        private void OnEnable() => 
            _triggerObserver.Entered += PlayerApproached;

        private void OnDisable() => 
            _triggerObserver.Entered -= PlayerApproached;

        private void PlayerApproached(Collider obj)
        {
            if (!obj.gameObject.TryGetComponent(out IDamageable damageable)) 
                return;
            
            _enemyFollower.Block();
            _enemyAttacker.SetTarget(damageable);
        }
    }
}