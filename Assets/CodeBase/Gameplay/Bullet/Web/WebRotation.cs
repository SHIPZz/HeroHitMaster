using CodeBase.Constants;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet.Web
{
    public class WebRotation : MonoBehaviour
    {
        private readonly Vector3 _rotationToFloor = new Vector3(90, 0, 0);

        private Rigidbody _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (other.collider.gameObject.layer == LayerId.Floor)
            {
                _rigidBody.DORotate(_rotationToFloor, 0f);
            }
        }
    }
}