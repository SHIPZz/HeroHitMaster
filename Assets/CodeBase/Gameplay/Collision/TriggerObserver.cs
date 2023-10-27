using System;
using UnityEngine;

namespace CodeBase.Gameplay.Collision
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> Entered;
        public event Action<Collider> Exited;
        public event Action<UnityEngine.Collision> CollisionEntered; 
        public event Action<UnityEngine.Collision> CollisionExited; 

        private void OnCollisionEnter(UnityEngine.Collision other) => 
            CollisionEntered?.Invoke(other);

        private void OnCollisionExit(UnityEngine.Collision other) => 
            CollisionExited?.Invoke(other);

        private void OnTriggerEnter(Collider other) =>
            Entered?.Invoke(other);

        private void OnTriggerExit(Collider other) =>
            Exited?.Invoke(other);
    }
}