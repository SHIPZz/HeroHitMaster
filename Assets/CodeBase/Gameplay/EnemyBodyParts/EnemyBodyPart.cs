using System;
using System.Collections.Generic;
using CodeBase.Enums;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPart : MonoBehaviour
    {
        private const float AutoDestroyDelay = 10f;
        private const float PositionY = 1;
        private const float Force = 400f;
        private const float RandomValueForVector = 1f;

        [SerializeField] private float _transformUpPosition;
        [SerializeField] private List<Rigidbody> _parts;

        [field: SerializeField] public EnemyTypeId EnemyTypeId { get; private set; }
        
        public void SetHeightPosition() =>
            transform.position += new Vector3(0, _transformUpPosition, 0);

        public async void Enable()
        {
            gameObject.SetActive(true);

            _parts.ForEach(x =>
            {
                x.isKinematic = false;
                Vector3 randomDirection = new Vector3(Random.Range(-RandomValueForVector, RandomValueForVector),
                    PositionY,
                    Random.Range(-RandomValueForVector, RandomValueForVector));
                x.AddForce(randomDirection * Force, ForceMode.Force);
            });

            await UniTask.WaitForSeconds(AutoDestroyDelay);
            _parts.ForEach(x => x.gameObject.SetActive(false));
        }
    }
}