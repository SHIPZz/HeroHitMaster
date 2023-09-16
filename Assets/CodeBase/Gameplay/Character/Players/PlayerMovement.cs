using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using CodeBase.Services;
using Zenject;

namespace CodeBase.Gameplay.Character.Players
{
    public class PlayerMovement : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private ICoroutineStarter _coroutineStarter;
        private TargetMovementStorage _targetMovementStorage;
        private int _currentTarget = -1;

        [Inject]
        private void Construct(ICoroutineStarter coroutineStarter,
            TargetMovementStorage targetMovementStorage)
        {
            _targetMovementStorage = targetMovementStorage;
            _coroutineStarter = coroutineStarter;
        }

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
        }

        public void MoveToNextZone()
        {
            _currentTarget++;

            _navMeshAgent.enabled = true;
            Vector3 targetPosition = _targetMovementStorage.Targets[_currentTarget].position;
            Move(targetPosition);
        }

        private void Move(Vector3 target)
        {
            _coroutineStarter.StartCoroutine(MoveCoroutine(target));
        }

        private IEnumerator MoveCoroutine(Vector3 target)
        {
            while (Vector3.Distance(_navMeshAgent.transform.position, target) > 2.2f)
            {
                if (!_navMeshAgent.isActiveAndEnabled)
                    break;
                
                _navMeshAgent.SetDestination(target);

                yield return null;
            }
        }
    }
}