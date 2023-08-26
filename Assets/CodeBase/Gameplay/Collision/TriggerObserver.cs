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

        private void OnCollisionEnter(UnityEngine.Collision other) => 
            CollisionEntered?.Invoke(other);

        private void OnTriggerEnter(Collider other) =>
            Entered?.Invoke(other);

        private void OnTriggerExit(Collider other) =>
            Exited?.Invoke(other);
    }
}