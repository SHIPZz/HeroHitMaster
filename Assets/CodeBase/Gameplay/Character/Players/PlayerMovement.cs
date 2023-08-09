﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using CodeBase.Services;

namespace CodeBase.Gameplay.Character.Players
{
    public class PlayerMovement : ITickable
    {
        private NavMeshAgent _navMeshAgent;
        private ICoroutineStarter _coroutineStarter;
        private TargetMovementStorage _targetMovementStorage;
        private int _currentTarget = -1;

        public PlayerMovement(NavMeshAgent navMeshAgent, ICoroutineStarter coroutineStarter,
            TargetMovementStorage targetMovementStorage)
        {
            _targetMovementStorage = targetMovementStorage;
            _navMeshAgent = navMeshAgent;
            _coroutineStarter = coroutineStarter;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.F))
                MoveToNextZone();
        }

        public void MoveToNextZone()
        {
            _currentTarget++;
            
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
                _navMeshAgent.SetDestination(target);

                yield return null;
            }
        }
    }
}