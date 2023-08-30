using System;
using CodeBase.Gameplay.Collision;
using UnityEngine;

namespace CodeBase.Services.SaveSystems
{
    public class SaveTriggerOnLevelEnd : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;

        public event Action PlayerEntered;

        private void OnEnable() => 
            _triggerObserver.Entered += OnPlayerEntered;

        private void OnDisable() => 
            _triggerObserver.Entered -= OnPlayerEntered;

        private void OnPlayerEntered(Collider obj)
        {
            PlayerEntered?.Invoke();
        }
    }
}