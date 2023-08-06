using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Gameplay.Character.Players
{
    public class PlayerTestMovement : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private AnimationCurve _animationCurve;

        private NavMeshAgent _navMeshAgent;
        private float _expiredTime;
        private float _duration = 1;
        private bool _isJumping = false;
        private float _jumpStartY;
        private float _jumpHeight = 5;
        private float _jumpDuration = 1;
        private Rigidbody _rb;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.autoTraverseOffMeshLink = false;
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // if (_navMeshAgent.isOnOffMeshLink && !_isJumping)
            // {
            //     _isJumping = true;
            // }
            //
            // if (_isJumping)
            // {
            //     transform.DOJump(_navMeshAgent.currentOffMeshLinkData.endPos, 0, 0, 2);
            //     print(_isJumping + " SET FALSE");
            // }
            //
            // _isJumping = false;
        }
    }
}