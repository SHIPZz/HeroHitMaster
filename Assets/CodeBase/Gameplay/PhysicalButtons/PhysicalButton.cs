using System;
using CodeBase.Gameplay.Collision;
using UnityEngine;

namespace CodeBase.Gameplay.PhysicalButtons
{
    [RequireComponent(typeof(TriggerObserver))]
    public class PhysicalButton : MonoBehaviour
    {
        private TriggerObserver _triggerObserver;

        public event Action Pressed;

        private void Awake()
        {
            _triggerObserver = GetComponent<TriggerObserver>();
        }

        private void OnEnable()
        {
            _triggerObserver.CollisionEntered += Press;
        }

        private void OnDisable()
        {
            _triggerObserver.CollisionEntered -= Press;
        }

        private void Press(UnityEngine.Collision obj)
        {
            if (obj.gameObject.TryGetComponent(out Bullet.Bullet bullet))
                Pressed?.Invoke();
        }
    }
}