using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Data;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyFollower : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private bool _isBlocked = true;
        private AggroZone _aggroZone;
        private Vector3 _target;
        private EnemyAnimator _enemyAnimator;
        private bool _isInitialized;

        [Inject]
        public void Construct(NavMeshAgent navMeshAgent, EnemyTypeId enemyTypeId,
            EnemyStaticDataService enemyStaticDataService)
        {
            _navMeshAgent = navMeshAgent;
            _navMeshAgent.speed = enemyStaticDataService.GetEnemyData(enemyTypeId).Speed;
        }

        private void Update()
        {
            if (_target == Vector3.zero || _isBlocked || !_navMeshAgent.isActiveAndEnabled)
                return;

            _navMeshAgent.SetDestination(_target);
        }

        private void OnDisable()
        {
            if (_aggroZone is null)
                return;

            _aggroZone.PlayerEntered -= SetTarget;
        }

        public void SetAggroZone(AggroZone aggroZone)
        {
            _aggroZone = aggroZone;
            _aggroZone.PlayerEntered += SetTarget;
        }

        public void Block()
        {
            if (!_navMeshAgent.isActiveAndEnabled)
            {
                _isBlocked = true;
                return;
            }

            _navMeshAgent.isStopped = true;
            _navMeshAgent.SetDestination(transform.position);
            _navMeshAgent.velocity = Vector3.zero;
            _isBlocked = true;
        }

        public void Unblock()
        {
            if (!_navMeshAgent.isActiveAndEnabled)
                return;

            _navMeshAgent.isStopped = false;
            _isBlocked = false;
        }

        private void SetTarget(Player player) => 
            _target = player.transform.position;
    }
}