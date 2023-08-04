﻿using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    public class DestroyableObjectBodyPart : MonoBehaviour
    {
        private const float PositionY = 1;
        private const float Force = 200f;
        private const float RandomValueForVector = 1f;
        
        [SerializeField] private List<Rigidbody> _parts;
        [field: SerializeField] public DestroyableObjectTypeId DestroyableObjectTypeId { get; private set; }
        
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