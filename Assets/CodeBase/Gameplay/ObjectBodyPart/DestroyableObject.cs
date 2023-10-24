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

        private IMaterialChanger _materialChanger;
        private bool _canDestroy = true;

        public event Action<DestroyableObjectTypeId> Destroyed;

        private void Awake() => 
            _materialChanger = GetComponent<IMaterialChanger>();

        private void OnEnable() =>
            _materialChanger.StartedChanged += BlockDestruction;

        private void OnDisable() =>
            _materialChanger.StartedChanged -= BlockDestruction;

        private void BlockDestruction() =>
            _canDestroy = false;

        public void Explode()
        {
            if (!_canDestroy)
                return;

            Destroyed?.Invoke(DestroyableObjectTypeId);

            if (_destroyDelay != 0)
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            
            Destroy(gameObject, _destroyDelay);
        }

        public void TakeDamage(int value)
        {
            if (!_canDestroy)
                return;

            Destroyed?.Invoke(DestroyableObjectTypeId);
            
            if (_destroyDelay != 0)
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            
            Destroy(gameObject, _destroyDelay);
        }
    }
}