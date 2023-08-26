using System;
using CodeBase.Gameplay.MaterialChanger;
using UnityEngine;

namespace CodeBase.Gameplay.Collision
{
    public class NonCollisionOnMaterialChanged : MonoBehaviour
    {
        private IMaterialChanger _materialChanger;
        private Collider _collider;

        private void Awake()
        {
            _materialChanger = GetComponent<IMaterialChanger>();
            _collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            _materialChanger.StartedChanged += DisableCollider;
        }

        private void OnDisable()
        {
            _materialChanger.StartedChanged -= DisableCollider;
        }

        private void DisableCollider() =>
            _collider.enabled = false;
    }
}