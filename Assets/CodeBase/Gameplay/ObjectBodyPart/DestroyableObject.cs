using System;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.MaterialChanger;
using UnityEngine;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    [RequireComponent(typeof(IMaterialChanger))]
    public class DestroyableObject : MonoBehaviour, IExplodable, IDamageable
    {
        [field: SerializeField] public DestroyableObjectTypeId DestroyableObjectTypeId { get; private set; }
        [SerializeField] private float _destroyDelay;
        [SerializeField] private bool _needActiveAfterMaterialChange = false;

        public bool MaterialChanged { get; private set; }

        private IMaterialChanger _materialChanger;
        private bool _canDestroy = true;

        public event Action<DestroyableObjectTypeId> Destroyed;

        private void Awake() =>
            _materialChanger = GetComponent<IMaterialChanger>();

        private void OnEnable()
        {
            _materialChanger.StartedChanged += BlockDestruction;
            _materialChanger.Completed += SetActive;
        }

        private void SetActive()
        {
            if (!_needActiveAfterMaterialChange)
                return;

            gameObject.SetActive(true);
            GetComponent<Collider>().enabled = false;
        }

        private void OnDisable()
        {
            _materialChanger.StartedChanged -= BlockDestruction;
            _materialChanger.Completed -= SetActive;
        }

        private void BlockDestruction()
        {
            MaterialChanged = true;
            Destroyed?.Invoke(DestroyableObjectTypeId);
            _canDestroy = false;
        }

        public void Explode()
        {
            if (!_canDestroy)
                return;

            Destroyed?.Invoke(DestroyableObjectTypeId);

            if (_destroyDelay != 0)
            {
                DisableVisibility();
                return;
            }

            Destroy(gameObject, _destroyDelay);
        }

        public void TakeDamage(int value)
        {
            if (!_canDestroy)
                return;

            Destroyed?.Invoke(DestroyableObjectTypeId);

            if (_destroyDelay != 0)
            {
                DisableVisibility();
                return;
            }

            Destroy(gameObject, _destroyDelay);
        }

        private void DisableVisibility()
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.localScale = Vector3.zero;
        }
    }
}