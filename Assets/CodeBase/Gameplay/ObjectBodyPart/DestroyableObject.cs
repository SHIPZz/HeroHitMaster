using System;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.MaterialChanger;
using UnityEngine;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    public class DestroyableObject : MonoBehaviour, IDestroyable, IDamageable
    {
        [field: SerializeField] public DestroyableObjectTypeId DestroyableObjectTypeId { get; private set; }

        private IMaterialChanger _materialChanger;
        private bool _canDestroy;

        public event Action<DestroyableObjectTypeId> Destroyed;

        private void Awake() =>
            _materialChanger = GetComponent<IMaterialChanger>();

        private void OnEnable() =>
            _materialChanger.StartedChanged += BlockDestruction;

        private void OnDisable() =>
            _materialChanger.StartedChanged -= BlockDestruction;

        private void BlockDestruction() =>
            _canDestroy = false;

        public void Destroy()
        {
            if(!_canDestroy)
                return;
            
            Destroyed?.Invoke(DestroyableObjectTypeId);
            Destroy(gameObject);
        }

        public void TakeDamage(int value)
        {
            if(!_canDestroy)
                return;
            
            Destroyed?.Invoke(DestroyableObjectTypeId);
            Destroy(gameObject);
        }
    }
}