using System;
using CodeBase.Gameplay.Character;
using UnityEngine;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    public class DestroyableObject : MonoBehaviour, IDestroyable, IDamageable
    {
        [field: SerializeField] public DestroyableObjectTypeId DestroyableObjectTypeId { get; private set; }

        public event Action<DestroyableObjectTypeId> Destroyed;

        public void Destroy()
        {
            Destroyed?.Invoke(DestroyableObjectTypeId);
            Destroy(gameObject);
        }

        public void TakeDamage(int value)
        {
            Destroyed?.Invoke(DestroyableObjectTypeId);
            Destroy(gameObject);
        }
    }
}