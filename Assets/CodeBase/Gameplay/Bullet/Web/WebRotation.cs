using UnityEngine;

namespace CodeBase.Gameplay.Bullet.Web
{
    public class WebRotation : MonoBehaviour
    {
        private Rigidbody _rigidBody;

        private void Awake() => 
            _rigidBody = GetComponent<Rigidbody>();

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            Quaternion targetRotation = Quaternion.LookRotation(other.GetContact(0).normal);
            _rigidBody.isKinematic = true;
            transform.rotation = targetRotation;
        }
    }
}