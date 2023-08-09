using CodeBase.Enums;
using CodeBase.Gameplay.Collision;
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
        private TriggerObserver _triggerObserver;
        private Vector3 _target;
        private EnemyAnimator _enemyAnimator;
        private bool _isInitialized;

        [Inject]
        public void Construct(NavMeshAgent navMeshAgent, EnemyTypeId enemyTypeId, EnemyStaticDataService enemyStaticDataService)
        {
            _navMeshAgent = navMeshAgent;
            _navMeshAgent.speed = enemyStaticDataService.GetEnemyData(enemyTypeId).Speed;
        }

        private void Update()
        {
            if (_target == Vector3.zero || _isBlocked)
                return;
            
            _navMeshAgent.SetDestination(_target);
        }

        private void OnDisable()
        {
            _triggerObserver.Entered -= SetTarget;
        }

        public void SetAggroZone(TriggerObserver aggroZone)
        {
            _triggerObserver = aggroZone;
            _triggerObserver.Entered += SetTarget;
        }

        public void InitMovement() =>
            _isInitialized = true;

        public void Block()
        {
            if(!_navMeshAgent.isActiveAndEnabled)
                return;
            
            _navMeshAgent.isStopped = true;
            _navMeshAgent.velocity = Vector3.zero;
            _isBlocked = true;
        }

        public void Unblock()
        {
            _navMeshAgent.isStopped = false;
            Debug.Log("unblock");
            _isBlocked = false;
        }

        private void SetTarget(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out Players.Player player))
                return;

            _target = player.transform.position;
        }
    }
}