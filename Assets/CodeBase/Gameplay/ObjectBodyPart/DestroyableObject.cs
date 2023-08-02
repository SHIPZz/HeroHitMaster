using System;
using UnityEngine;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    public class DestroyableObject : MonoBehaviour, IDestroyable
    {
        [field: SerializeField] public DestroyableObjectTypeId DestroyableObjectTypeId { get; private set; }
        
        public event Action<DestroyableObjectTypeId> Destroyed;
        
        public void Destroy()
        {
            Destroy(gameObject);
            Destroyed?.Invoke(DestroyableObjectTypeId);
        }
    }
}