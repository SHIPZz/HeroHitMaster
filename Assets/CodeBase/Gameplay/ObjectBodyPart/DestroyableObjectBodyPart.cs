using System;
using System.Collections.Generic;
using CodeBase.Constants;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    public class DestroyableObjectBodyPart : MonoBehaviour
    {
        private const float AutoDestroyDelay = 10;

        [SerializeField] private List<Rigidbody> _parts;
        [SerializeField] private float _minimalForce = 300f;
        [SerializeField] private float _maxForce = 650f;
        
        [field: SerializeField] public DestroyableObjectTypeId DestroyableObjectTypeId { get; private set; }

        private void Awake() =>
            gameObject.layer = LayerId.DestroyableObjectPart;

        public void Enable()
        {
            float force = Random.Range(_minimalForce, _maxForce);

            _parts.ForEach(x =>
            {
                x.isKinematic = false;

                Vector3 randomDirection = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)
                ).normalized;

                x.AddForce(randomDirection * force, ForceMode.Force);
            });

            DOTween.Sequence().AppendInterval(AutoDestroyDelay).OnComplete(Disable);
        }

        private void Disable()
        {
            DOTween.Kill(transform);
            gameObject.SetActive(false);
        }
    }
}