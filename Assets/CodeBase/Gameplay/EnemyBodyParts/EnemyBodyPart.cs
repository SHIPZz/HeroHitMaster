using System.Collections.Generic;
using Enums;
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
                x.AddForce(randomDirection * Force, ForceMode.Force);
            });
        }

        public void Disable() =>
            gameObject.SetActive(false);
    }
}