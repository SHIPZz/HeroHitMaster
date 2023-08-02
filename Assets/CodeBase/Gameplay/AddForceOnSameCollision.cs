using System.Collections.Generic;
using CodeBase.Gameplay.Collision;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay
{
    public class AddForceOnSameCollision : MonoBehaviour
    {
        private const float Force = 80;

        private TriggerObserver _triggerObserver;
        private Rigidbody _rigidbody;

        private readonly List<Vector3> _rotationVectors = new()
        {
            new Vector3(0, 0, 360),
            new Vector3(0, 360, 0),
            new Vector3(360, 0, 0)
        };

        [Inject]
        private void Construct(TriggerObserver triggerObserver)
        {
            _triggerObserver = triggerObserver;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void OnEnable()
        {
            _triggerObserver.Entered += OnTriggerEntered;
        }

        public void OnDisable()
        {
            _triggerObserver.Entered -= OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider obj)
        {
            if (obj.gameObject.layer != gameObject.layer)
                return;

            var targetDirection = new Vector3(Random.Range(-6, 20f), Random.Range(4, 7), Random.Range(4, 15));
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(targetDirection * Force, ForceMode.Force);

            Rotate();

            DOTween.Sequence().AppendInterval(5f).OnComplete(() =>
            {
                _rigidbody.isKinematic = true;
                DOTween.Kill(transform);
            });
        }

        private void Rotate()
        {
            int randomValue = Random.Range(0, _rotationVectors.Count - 1);
            Vector3 randomVector = _rotationVectors[randomValue];

            transform.DOLocalRotate(randomVector, 1f).SetRelative(true).SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}