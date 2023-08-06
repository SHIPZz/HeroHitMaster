using System;
using UnityEngine;

namespace CodeBase.Gameplay.Collision
{
    public class DisableBulletOnSameCollision : MonoBehaviour
    {
        private TriggerObserver _triggerObserver;

        private void Awake() => _triggerObserver = GetComponent<TriggerObserver>();

        private void OnEnable() => 
            _triggerObserver.Entered += Disable;

        private void OnDisable() => 
            _triggerObserver.Entered -= Disable;

        private void Disable(Collider obj)
        {
            if (obj.gameObject.layer == gameObject.layer)
                gameObject.SetActive(false);
        }
    }
}