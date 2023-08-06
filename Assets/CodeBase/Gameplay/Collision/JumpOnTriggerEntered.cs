using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Gameplay.Collision
{
    public class JumpOnTriggerEntered : MonoBehaviour
    {
        [SerializeField] private Transform _nextPosition;
        [SerializeField] private float _delayBeforeJump;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _enableNavMeshDelay = 1.5f;

        private TriggerObserver _triggerObserver;
        private bool _isJumped;

        private void Awake()
        {
            _triggerObserver = GetComponent<TriggerObserver>();
        }

        private void OnEnable()
        {
            _triggerObserver.Entered += Jump;
        }

        private void OnDisable()
        {
            _triggerObserver.Entered -= Jump;
        }

        private void Jump(Collider player)
        {
            var navMesh =  player.gameObject.GetComponent<NavMeshAgent>();
            navMesh.enabled = false;

            EnableNavMeshWithDuration(navMesh);
            
            DoJump(player);
        }

        private void DoJump(Collider obj) =>
            DOTween.Sequence()
                .AppendInterval(_delayBeforeJump)
                .OnComplete(() =>
                    obj.gameObject.transform.DOJump(_nextPosition.position, 0, 0, _jumpDuration));

        private void EnableNavMeshWithDuration(NavMeshAgent navMesh) =>
            DOTween
                .Sequence()
                .AppendInterval(_enableNavMeshDelay)
                .OnComplete(() => navMesh.enabled = true);
    }
}