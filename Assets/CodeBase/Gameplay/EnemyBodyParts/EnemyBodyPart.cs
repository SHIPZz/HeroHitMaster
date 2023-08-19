using System;
using System.Collections.Generic;
using CodeBase.Enums;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPart : MonoBehaviour
    {
        private const float PositionY = 1;
        private const float Force = 200f;
        private const float RandomValueForVector = 1f;

        [SerializeField] private float _transformUpPosition;
        [SerializeField] private List<Rigidbody> _parts;

        [field: SerializeField] public EnemyTypeId EnemyTypeId { get; private set; }

        private readonly List<Vector3> _rotationVectors = new()
        {
            new Vector3(0, 360, 0)
        };

        public void SetHeightPosition() =>
            transform.position += new Vector3(0, _transformUpPosition, 0);

        public void Enable()
        {
            gameObject.SetActive(true);
            _parts.ForEach(x =>
            {
                x.isKinematic = false;
                Vector3 randomDirection = new Vector3(Random.Range(-RandomValueForVector, RandomValueForVector),
                    PositionY,
                    Random.Range(-RandomValueForVector, RandomValueForVector));

                int randomValue = Random.Range(0, _rotationVectors.Count - 1);
                Vector3 randomVector = _rotationVectors[randomValue];
                x.transform.DOLocalRotate(randomVector, 1.5f).SetRelative(true).SetEase(Ease.Linear);
                x.AddForce(randomDirection * Force, ForceMode.Force);
            });
        }

        public void Disable()
        {
            DOTween.Kill(transform);
            // gameObject.SetActive(false);
        }
    }
}