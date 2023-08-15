using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Collision
{
    [RequireComponent(typeof(TriggerObserver))]
    public class MovePlayerOnTrigger : MonoBehaviour
    {
        private const float MoveDuration = 4f;

        [SerializeField] private Transform _target;

        private TriggerObserver _triggerObserver;

        private void Awake() =>
            _triggerObserver = GetComponent<TriggerObserver>();

        private void OnEnable() =>
            _triggerObserver.Entered += MovePlayerToTarget;

        private void OnDisable() =>
            _triggerObserver.Entered -= MovePlayerToTarget;

        private void MovePlayerToTarget(Collider player)
        {
            DOTween.Sequence().AppendInterval(1.5f).OnComplete(() => Test(player));
        }

        private void Test(Collider player)
        {
            player.transform.SetParent(transform);
            GetComponent<Rigidbody>().DOMoveZ(_target.position.z, MoveDuration)
                .OnComplete(() => player.transform.SetParent(null));
        }
    }
}