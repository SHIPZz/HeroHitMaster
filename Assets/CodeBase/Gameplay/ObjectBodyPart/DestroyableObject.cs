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
            Destroy(gameObject);
            Destroyed?.Invoke(DestroyableObjectTypeId);
        }

        public void TakeDamage(int value)
        {
            Destroy(gameObject);
            Destroyed?.Invoke(DestroyableObjectTypeId);
        }
    }
}