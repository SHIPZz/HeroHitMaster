using System;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet.Web
{
    public class WebRotation : MonoBehaviour
    {
        [SerializeField] private Vector3 _initialRotation = new Vector3(0, -90, 0);
        private Rigidbody _rigidBody;

        private void Awake() => 
            _rigidBody = GetComponent<Rigidbody>();

        private void OnEnable()
        {
            transform.localEulerAngles = _initialRotation;
            _rigidBody.isKinematic = false;
        }

        private void OnDisable() => 
            _rigidBody.isKinematic = false;

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            Quaternion targetRotation = Quaternion.LookRotation(other.GetContact(0).normal);
            _rigidBody.isKinematic = true;
            transform.rotation = targetRotation;
        }
    }
}