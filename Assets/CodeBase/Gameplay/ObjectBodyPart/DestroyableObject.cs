using System;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.MaterialChanger;
using UnityEngine;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    [RequireComponent(typeof(IMaterialChanger), typeof(MeshRenderer), typeof(Collider))]
    public class DestroyableObject : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public DestroyableObjectTypeId DestroyableObjectTypeId { get; private set; }

        public bool MaterialChanged { get; private set; }

        private IMaterialChanger _materialChanger;
        private bool _canDestroy = true;
        private MeshRenderer _meshRenderer;
        private Collider _collider;
        private bool _isMaterialChanged;

        public event Action<DestroyableObjectTypeId> Destroyed;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
            _materialChanger = GetComponent<IMaterialChanger>();
        }

        private void OnEnable()
        {
            _materialChanger.StartedChanged += BlockDestruction;
            _materialChanger.Completed += OnMaterialChanged;
        }

        private void OnDisable()
        {
            _materialChanger.StartedChanged -= BlockDestruction;
            _materialChanger.Completed -= OnMaterialChanged;
        }

        private void OnMaterialChanged()
        {
            Destroyed?.Invoke(DestroyableObjectTypeId);
            _isMaterialChanged = true;
        }

        private void BlockDestruction()
        {
            _collider.enabled = false;
            MaterialChanged = true;
            _meshRenderer.enabled = true;
            Destroyed?.Invoke(DestroyableObjectTypeId);
            _isMaterialChanged = true;
        }

        public void TakeDamage(int value)
        {
            if (_isMaterialChanged)
                return;

            DisableVisibility();
            Destroyed?.Invoke(DestroyableObjectTypeId);
        }

        private void DisableVisibility()
        {
            _meshRenderer.enabled = false;
            _collider.enabled = false;
        }
    }
}