using CodeBase.Gameplay.ObjectBodyPart;
using UnityEngine;

namespace CodeBase.Gameplay.Collision
{
    public class DisableBulletOnSpecificCollision : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;

        private void OnEnable() =>
            _triggerObserver.CollisionEntered += TryDisable;

        private void OnDisable() =>
            _triggerObserver.CollisionEntered -= TryDisable;

        private void TryDisable(UnityEngine.Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out DestroyableObject destroyableObj))
            {
                gameObject.SetActive(false);
            }
        }
    }
}