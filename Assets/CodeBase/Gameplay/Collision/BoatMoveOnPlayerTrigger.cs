using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Collision
{
    [RequireComponent(typeof(TriggerObserver))]
    public class BoatMoveOnPlayerTrigger : MonoBehaviour
    {
        private const float MoveDuration = 4f;

        [SerializeField] private Transform _target;

        private TriggerObserver _triggerObserver;

        public event Action MovementStarted;
        public event Action MovementCompleted;

        private void Awake() =>
            _triggerObserver = GetComponent<TriggerObserver>();

        private void OnEnable() =>
            _triggerObserver.Entered += MovePlayerToTarget;

        private void OnDisable() =>
            _triggerObserver.Entered -= MovePlayerToTarget;

        private void MovePlayerToTarget(Collider player)
        {
            DOTween.Sequence().AppendInterval(1.5f).OnComplete(() => Move(player));
        }

        private void Move(Collider player)
        {
            player.transform.SetParent(transform);
            MovementStarted?.Invoke();

            transform.DOMoveY(transform.position.y + 0.5f, 0.5f)
                .OnComplete(() => transform.DOMoveY(transform.position.y - 0.3f, 0.5f))
                .SetLoops(-1, LoopType.Yoyo);
            
            transform.DOMoveZ(_target.position.z, MoveDuration)
                .OnComplete(() =>
                {
                    player.transform.SetParent(null);
                    MovementCompleted?.Invoke();
                    DOTween.Kill(transform);
                });
        }
    }
}