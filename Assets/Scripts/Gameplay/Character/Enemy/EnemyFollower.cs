using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyFollower : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private bool _isBlocked = false;
        private TriggerObserver _triggerObserver;
        private Vector3 _target;
        private EnemyAnimator _enemyAnimator;

        [Inject]
        public void Construct(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
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

        public void Block()
        {
            _navMeshAgent.destination = _navMeshAgent.transform.position;
            _isBlocked = true;
        }

        public void Unblock() =>
            _isBlocked = false;

        private void SetTarget(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out Players.Player player))
                return;

            _target = player.transform.position;
        }
    }
}