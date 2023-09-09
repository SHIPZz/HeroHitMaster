using System;
using CodeBase.Constants;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Gameplay.Collision
{
    [RequireComponent(typeof(TriggerObserver), typeof(BoxCollider))]
    public class JumpOnTriggerEntered : MonoBehaviour
    {
        [SerializeField] private Transform _nextPosition;
        [SerializeField] private float _delayBeforeJump;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _enableNavMeshDelay = 1.5f;
        [SerializeField] private float _jumpPower;
        
        private TriggerObserver _triggerObserver;
        private bool _isJumped;
        
        public event Action Jumped;

        private void Awake()
        {
            _triggerObserver = GetComponent<TriggerObserver>();
            gameObject.layer = LayerId.JumpTrigger;
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnEnable() =>
            _triggerObserver.Entered += Jump;

        private void OnDisable() =>
            _triggerObserver.Entered -= Jump;

        private void Jump(Collider player)
        {
            var navMesh = player.gameObject.GetComponent<NavMeshAgent>();
            navMesh.enabled = false;

            DoJump(player);
        }

        private void DoJump(Collider obj)
        {
            DOTween.Sequence()
                .AppendInterval(_delayBeforeJump)
                .OnComplete(() =>
                {
                    obj.transform
                        .DOJump(_nextPosition.position, _jumpPower, 0, _jumpDuration);
                    Jumped?.Invoke();
                });
        }
    }
}